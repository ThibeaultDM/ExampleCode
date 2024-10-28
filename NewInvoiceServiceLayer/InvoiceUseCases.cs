﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        public InvoiceUseCases(IInvoiceHeaderRepository headerRepository, IInvoiceNumberRepository numberRepository, IInvoiceExceptionRepository exceptionRepository, IMapper mapper)
        {
            _headerRepository = headerRepository;
            _numberRepository = numberRepository;
            _exceptionRepository = exceptionRepository;
            _mapper = mapper;
        }

        public async Task<BO_InvoiceHeader> UC_301_001_CreateInvoiceHeaderAsync(string input)
        {
            BO_InvoiceHeader invoiceHeaderBO = new(input);

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
                    await _exceptionRepository.SaveInvoiceExceptionAsync(new()
                    {
                        Type = 4,
                        NameSpace = "NewInvoiceServiceLayer.Service.InvoiceUseCases",
                        Message = "No Valid invoiceHeader was created",
                        InputParameters = input
                    });
                }
            }
            catch (Exception ex)
            {
                await SaveErrorException(input, ex);

                invoiceHeaderBO = HandleException(invoiceHeaderBO, ex);
            }

            return invoiceHeaderBO;
        }

        // Normally should all have input and response model and follow UC_301_001_CreateInvoiceHeaderAsync design.
        public async Task<BO_InvoiceHeader> UC_301_002_AddInvoiceLineToHeaderAsync(BO_InvoiceLine input)
        {
            BO_InvoiceHeader invoiceHeaderBO = new();
            string dontWantToTouchApiAndUiModelSetUp = "";
            try
            {
                DO_InvoiceHeader invoiceHeaderDo = await _headerRepository.FindInvoiceHeaderAsync(input.InvoiceHeaderId);

                if (invoiceHeaderDo != null)
                {
                    invoiceHeaderBO = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDo);

                    if (input.Valid)
                    {
                        DO_InvoiceLine invoiceLineDo = _mapper.Map<DO_InvoiceLine>(input);
                        invoiceHeaderBO.AddInvoiceLineToHeader(input);

                        invoiceHeaderDo = _mapper.Map<DO_InvoiceHeader>(invoiceHeaderBO);
                        invoiceHeaderDo = await _headerRepository.UpdateInvoiceHeaderAsync(invoiceHeaderDo);
                    }
                    else
                    {
                        input.BrokenRules.ForEach(br => dontWantToTouchApiAndUiModelSetUp += $"{br.PropertyName}: {br.FailedMessage}\n");

                        throw new Exception(dontWantToTouchApiAndUiModelSetUp);
                    }
                }
                else
                {
                    invoiceHeaderBO.BrokenRules.Add(new() { PropertyName = "Not Found Error", FailedMessage = "InvoiceHeader not found" });

                    await SaveHeaderNotFoundException(input.ToString());
                }
            }
            catch (Exception ex)
            {
                await SaveErrorException(input.ToString(), ex);

                invoiceHeaderBO = HandleException(invoiceHeaderBO, ex);
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
                    await SaveHeaderNotFoundException(toFind.ToString());
                    invoiceHeaderBO = null;
                }
            }
            catch (Exception ex)
            {
                await SaveErrorException(toFind.ToString(), ex);

                invoiceHeaderBO = HandleException(invoiceHeaderBO, ex);
            }

            return invoiceHeaderBO;
        }

        public async Task<BO_InvoiceHeader> UC_301_004_ArchiveJournalEntryForInvoiceAsync(Guid proxyCompanyId, Guid InvoiceHeaderId)
        {
            BO_InvoiceHeader invoiceHeaderBO = new();

            try
            {
                DO_InvoiceHeader invoiceHeaderDO = await _headerRepository.FindInvoiceHeaderAsync(InvoiceHeaderId);

                if (invoiceHeaderDO != null)
                {
                    invoiceHeaderDO.CompanyProxyId = proxyCompanyId;
                    // To avoid any duplicate line being added to the database when updating header
                    invoiceHeaderDO.InvoiceLines = null;

                    invoiceHeaderDO = await _headerRepository.UpdateInvoiceHeaderAsync(invoiceHeaderDO);
                    invoiceHeaderBO = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDO);
                }
                else
                {
                    await SaveHeaderNotFoundException($"{proxyCompanyId}, {InvoiceHeaderId}");

                    invoiceHeaderBO = null;
                }
            }
            catch (Exception ex)
            {
                await SaveErrorException($"{proxyCompanyId}, {InvoiceHeaderId}", ex);

                invoiceHeaderBO = HandleException(invoiceHeaderBO, ex);
            }

            return invoiceHeaderBO;
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
                invoiceHeaderBO = HandleException(invoiceHeaderBO, ex);
                //TODO doesn't really work here
                listInvoiceHeaderBO.Add(invoiceHeaderBO);
            }

            return listInvoiceHeaderBO;
        }

        #region Private Methodes

        private T HandleException<T>(T problemChild, Exception ex) where T : BusinessObjectBase
        {
            T result;

            if (ex.InnerException != null)
            {
                result = HandleException<T>(problemChild, ex.InnerException);
            }
            else
            {
                problemChild.BrokenRules.Add(new BrokenRule("none", ex.Message));
                result = problemChild;
            }

            return result;
        }

        private async Task SaveErrorException(string InputParameters, Exception ex)
        {
            await _exceptionRepository.SaveInvoiceExceptionAsync(new()
            {
                Type = 1,
                NameSpace = "NewInvoiceServiceLayer.Service.InvoiceUseCases",
                Message = ex.InnerException.Message,
                InputParameters = InputParameters
            });
        }

        private async Task SaveHeaderNotFoundException(string inputParameters)
        {
            await _exceptionRepository.SaveInvoiceExceptionAsync(new()
            {
                Type = 6,
                NameSpace = "NewInvoiceServiceLayer.Service.InvoiceUseCases",
                Message = "InvoiceHeader not found",
                InputParameters = inputParameters
            });
        }

        #endregion Private Methodes
    }
}