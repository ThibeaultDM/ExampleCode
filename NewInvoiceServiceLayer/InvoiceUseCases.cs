using AutoMapper;
using NewInvoiceBusinessLayer.Objects;
using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Objects;
using NewInvoiceServiceLayer.Interfaces;
using NewInvoiceServiceLayer.Objects;
using QueasoFramework.BusinessModels;
using QueasoFramework.BusinessModels.Rules;

namespace NewInvoiceServiceLayer.Service
{
    public class InvoiceUseCases : IInvoiceUseCases
    {
        private readonly IInvoiceHeaderRepository _headerRepository;
        private readonly IInvoiceNumberRepository _numberRepository;
        private readonly IInvoiceExceptionRepository _exceptionRepository;
        private readonly IJournalEntryRepository _journalEntryRepository;
        private readonly IMapper _mapper;

        public InvoiceUseCases(IInvoiceHeaderRepository headerRepository, IInvoiceNumberRepository numberRepository, IInvoiceExceptionRepository exceptionRepository, IJournalEntryRepository journalEntryRepository, IMapper mapper)
        {
            _headerRepository = headerRepository;
            _numberRepository = numberRepository;
            _exceptionRepository = exceptionRepository;
            _journalEntryRepository = journalEntryRepository;
            _mapper = mapper;
        }

        public async Task<BO_InvoiceHeader> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber, string proxyCompanyId)
        {
            BO_InvoiceHeader invoiceHeaderBO = new(vatNumber, proxyCompanyId);

            try
            {
                invoiceHeaderBO.InvoiceNumber = await _numberRepository.GetNextNumber();

                if (invoiceHeaderBO.Valid)
                {
                    DO_InvoiceHeader invoiceHeaderDo = _mapper.Map<DO_InvoiceHeader>(invoiceHeaderBO);
                    invoiceHeaderDo = await _headerRepository.CreateInvoiceHeaderAsync(invoiceHeaderDo);

                    invoiceHeaderBO = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDo);
                }
                else
                {
                    BO_InvoiceException invoiceExceptionBO = new()
                    {
                        Type = NewInvoiceBusinessLayer.Enums.InvoiceExceptionTypes.InvalidVATNumber,
                        NameSpace = "NewInvoiceServiceLayer.Service.InvoiceUseCases",
                        Message = "No Valid invoiceHeader was created",
                        InputParameters = $"{vatNumber}, {proxyCompanyId}"
                    };

                    DO_InvoiceException invoiceExceptionDO = _mapper.Map<DO_InvoiceException>(invoiceExceptionBO);
                    await _exceptionRepository.SaveInvoiceExceptionAsync(invoiceExceptionDO);
                }
            }
            catch (Exception ex)
            {
                await SaveErrorException($"{vatNumber}, {proxyCompanyId}", ex);

                invoiceHeaderBO = HandleCriticalErrorResponse(invoiceHeaderBO, ex);
            }

            return invoiceHeaderBO;
        }

        // Normally should all have input and response model and follow UC_301_001_CreateInvoiceHeaderAsync design.
        public async Task<BO_InvoiceHeader> UC_301_002_AddInvoiceLineToHeaderAsync(BO_InvoiceLine input)
        {
            BO_InvoiceHeader invoiceHeaderBO = new();
            try
            {
                DO_InvoiceHeader invoiceHeaderDo = await _headerRepository.FindInvoiceHeaderAsync(input.InvoiceHeaderId);

                if (invoiceHeaderDo != null)
                {
                    invoiceHeaderBO = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDo);

                    if (input.Valid)
                    {
                        invoiceHeaderBO.AddInvoiceLineToHeader(input);

                        invoiceHeaderDo = _mapper.Map<DO_InvoiceHeader>(invoiceHeaderBO);
                        invoiceHeaderDo = await _headerRepository.UpdateInvoiceHeaderAsync(invoiceHeaderDo);
                    }
                    else
                    {
                        await ResolveBusinessRulesBroken(input);
                    }
                }
                else
                {
                    invoiceHeaderBO = await ResolveInvoiceHeaderNotFound(input.InvoiceHeaderId.ToString(), invoiceHeaderBO);
                }
            }
            catch (Exception ex)
            {
                await SaveErrorException(input.ToString(), ex);

                invoiceHeaderBO = HandleCriticalErrorResponse(invoiceHeaderBO, ex);
            }

            return invoiceHeaderBO;
        }

        public async Task<BO_InvoiceHeader> UC_301_003_FindInvoiceHeaderAsync(Guid toFind)
        {
            BO_InvoiceHeader invoiceHeaderBO = new();

            try
            {
                DO_InvoiceHeader invoiceHeaderDo = await _headerRepository.FindInvoiceHeaderAsync(toFind);

                if (invoiceHeaderDo != null)
                {
                    invoiceHeaderBO = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDo);
                }
                else
                {
                    invoiceHeaderBO = await ResolveInvoiceHeaderNotFound(toFind.ToString(), invoiceHeaderBO);
                }
            }
            catch (Exception ex)
            {
                await SaveErrorException(toFind.ToString(), ex);

                invoiceHeaderBO = HandleCriticalErrorResponse(invoiceHeaderBO, ex);
            }

            return invoiceHeaderBO;
        }

        public async Task<BO_JournalEntry> UC_301_004_ArchiveJournalEntryForInvoiceAsync(Guid idJournalEntry, Guid idInvoiceHeader)
        {
            BO_JournalEntry journalEntryBO = new(idJournalEntry, idInvoiceHeader);

            try
            {
                DO_JournalEntry journalEntryDO = _mapper.Map<DO_JournalEntry>(journalEntryBO);
                journalEntryDO = await _journalEntryRepository.SaveJournalEntryAsync(journalEntryDO);

                journalEntryBO = _mapper.Map<BO_JournalEntry>(journalEntryDO);
            }
            catch (Exception ex)
            {
                await SaveErrorException($"idJournalEntry: {idJournalEntry}, idInvoiceHeader: {idInvoiceHeader}", ex);

                journalEntryBO = HandleCriticalErrorResponse(journalEntryBO, ex);
            }

            return journalEntryBO;
        }

        public async Task<List<BO_InvoiceHeader>> UC_301_005_GetAllInvoicesHeadersAsync()
        {
            List<BO_InvoiceHeader> listInvoiceHeaderBO = new();
            BO_InvoiceHeader invoiceHeaderBO = new();

            try
            {
                List<DO_InvoiceHeader> invoiceHeadersDo = await _headerRepository.GetInvoiceHeadersAsync();

                if (invoiceHeadersDo.Count > 0)
                {
                    listInvoiceHeaderBO = _mapper.Map<List<DO_InvoiceHeader>, List<BO_InvoiceHeader>>(invoiceHeadersDo);
                }
            }
            catch (Exception ex)
            {
                await SaveErrorException($"UC_301_005_GetAllInvoicesHeadersAsync has no InputParameters", ex);

                invoiceHeaderBO = HandleCriticalErrorResponse(invoiceHeaderBO, ex);
                //TODO doesn't really work here
                listInvoiceHeaderBO.Add(invoiceHeaderBO);
            }

            return listInvoiceHeaderBO;
        }

        #region Private Methodes

        private T HandleCriticalErrorResponse<T>(T problemChild, Exception ex) where T : BusinessObjectBase
        {
            T result;

            if (ex.InnerException != null)
            {
                result = HandleCriticalErrorResponse<T>(problemChild, ex.InnerException);
            }
            else
            {
                problemChild.BrokenRules.Add(new BrokenRule("none", ex.Message));
                result = problemChild;
            }

            return result;
        }

        private async Task SaveErrorException(string inputParameters, Exception ex)
        {
            BO_InvoiceException invoiceExceptionBO = new()
            {
                Type = NewInvoiceBusinessLayer.Enums.InvoiceExceptionTypes.Error,
                NameSpace = "NewInvoiceServiceLayer.Service.InvoiceUseCases",
                Message = ex.Message,
                InputParameters = inputParameters
            };

            DO_InvoiceException invoiceExceptionDO = _mapper.Map<DO_InvoiceException>(invoiceExceptionBO);
            await _exceptionRepository.SaveInvoiceExceptionAsync(invoiceExceptionDO);
        }

        private async Task SaveHeaderNotFoundException(string inputParameters)
        {
            BO_InvoiceException invoiceExceptionBO = new()
            {
                Type = NewInvoiceBusinessLayer.Enums.InvoiceExceptionTypes.HeaderNotFound,
                NameSpace = "NewInvoiceServiceLayer.Service.InvoiceUseCases",
                Message = "InvoiceHeader not found",
                InputParameters = inputParameters
            };

            DO_InvoiceException invoiceExceptionDO = _mapper.Map<DO_InvoiceException>(invoiceExceptionBO);
            await _exceptionRepository.SaveInvoiceExceptionAsync(invoiceExceptionDO);
        }

        private async Task ResolveBusinessRulesBroken(BO_InvoiceLine input)
        {
            string businessRuleViolationMessage = "";
            input.BrokenRules.ForEach(br => businessRuleViolationMessage += $"{br.PropertyName}: {br.FailedMessage}\n");

            BO_InvoiceException invoiceExceptionBO = new()
            {
                Type = NewInvoiceBusinessLayer.Enums.InvoiceExceptionTypes.BusinessRuleViolation,
                NameSpace = "NewInvoiceServiceLayer.Service.InvoiceUseCases",
                Message = businessRuleViolationMessage,
                InputParameters = input.ToString()
            };

            DO_InvoiceException invoiceExceptionDO = _mapper.Map<DO_InvoiceException>(invoiceExceptionBO);
            await _exceptionRepository.SaveInvoiceExceptionAsync(invoiceExceptionDO);
        }

        private async Task<BO_InvoiceHeader> ResolveInvoiceHeaderNotFound(string input, BO_InvoiceHeader invoiceHeaderBO)
        {
            invoiceHeaderBO.BrokenRules.Add(new() { PropertyName = "none", FailedMessage = "InvoiceHeader not found" });

            await SaveHeaderNotFoundException(input);
            return invoiceHeaderBO;
        }

        #endregion Private Methodes
    }
}