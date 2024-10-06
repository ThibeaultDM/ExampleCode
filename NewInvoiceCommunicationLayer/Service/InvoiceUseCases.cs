using AutoMapper;
using NewInvoiceCommunicationLayer.Interfaces;
using NewInvoiceCommunicationLayer.Models.Input;
using NewInvoiceCommunicationLayer.Models.Response;
using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Objects;
using NewInvoiceServiceLayer.Objects;
using QueasoFramework.Exceptions;

namespace NewInvoiceCommunicationLayer.Service
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

        public async Task<CreateInvoiceHeaderResponse> UC_301_001_CreateInvoiceHeaderAsync(CreateInvoiceHeaderInput input)
        {
            CreateInvoiceHeaderResponse response = new();
            BO_InvoiceHeader invoiceHeaderBo = new(input.VATNumber);

            try
            {
                invoiceHeaderBo.InvoiceNumber = await _numberRepository.GetNextNumber();

                if (invoiceHeaderBo.Valid)
                {
                    DO_InvoiceHeader invoiceHeaderDo = _mapper.Map<DO_InvoiceHeader>(invoiceHeaderBo);
                    invoiceHeaderDo = await _headerRepository.CreateInvoiceHeaderAsync(invoiceHeaderDo);

                    invoiceHeaderBo = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDo);
                }

                response = _mapper.Map<CreateInvoiceHeaderResponse>(invoiceHeaderBo);
                invoiceHeaderBo.BrokenRules.ForEach(br => response.SetErrors(new(br.PropertyName, br.FailedMessage)));
            }
            catch (Exception ex)
            {
                // TODO look at implementing this pattern.
                // response.SetErrors(new(null, $"A critical error occurred trying to create an invoiceHeader with VATNumber {input}, please contact administrator."));

                throw ex;
            }

            return response;
        }

        // Normally should all have input and response model and follow UC_301_001_CreateInvoiceHeaderAsync design.
        public async Task<BO_InvoiceHeader> UC_301_002_AddInvoiceLineToHeaderAsync(AddInvoiceLineToInvoiceHeaderInput input)
        {
            BO_InvoiceHeader response;

            try
            {
                DO_InvoiceHeader invoiceHeaderDo = await _headerRepository.FindInvoiceHeaderAsync(input.InvoiceHeaderId);

                if (invoiceHeaderDo != null)
                {
                    BO_InvoiceHeader invoiceHeaderBo = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDo);
                    BO_InvoiceLine invoiceLineBo = _mapper.Map<BO_InvoiceLine>(input);

                    if (invoiceLineBo.Valid)
                    {
                        DO_InvoiceLine invoiceLineDo = _mapper.Map<DO_InvoiceLine>(invoiceLineBo);
                        invoiceHeaderBo.AddInvoiceLineToHeader(invoiceLineBo);

                        invoiceHeaderDo = _mapper.Map<DO_InvoiceHeader>(invoiceHeaderBo);
                        invoiceHeaderDo = await _headerRepository.UpdateInvoiceHeaderAsync(invoiceHeaderDo);

                        response = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDo);
                    }
                    else
                    {
                        string dontWantToTouchApiAndUiModelSetUp = "";
                        invoiceLineBo.BrokenRules.ForEach(br => dontWantToTouchApiAndUiModelSetUp += $"{br.PropertyName}: {br.FailedMessage}\n");

                        throw new Exception(dontWantToTouchApiAndUiModelSetUp);
                    }
                }
                else
                {
                    response = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public async Task<BO_InvoiceHeader> UC_301_003_FindInvoiceHeaderAsync(GetInvoiceByNameInput toFind)
        {
            BO_InvoiceHeader response;

            try
            {
                DO_InvoiceHeader invoiceHeaderDo = await _headerRepository.FindInvoiceHeaderAsync(toFind.InvoiceHeaderId);

                if (invoiceHeaderDo != null)
                {
                    response = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDo);
                }
                else
                {
                    response = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public async Task<BO_InvoiceHeader> UC_301_004_ArchiveJournalEntryForInvoiceAsync(ArchiveInvoiceJournalEntryInput input)
        {
            BO_InvoiceHeader response;

            try
            {
                DO_InvoiceHeader invoiceHeaderDo = await _headerRepository.FindInvoiceHeaderAsync(input.InvoiceHeaderId);

                if (invoiceHeaderDo != null)
                {
                    invoiceHeaderDo.CompanyProxyId = input.proxyCompanyId;
                    // To avoid any duplicate line being added to the database when updating header
                    // TODO check this doesn't remove the invoiceLines in database, it shouldn't.
                    invoiceHeaderDo.InvoiceLines = null;
                    invoiceHeaderDo = await _headerRepository.UpdateInvoiceHeaderAsync(invoiceHeaderDo);
                    response = _mapper.Map<BO_InvoiceHeader>(invoiceHeaderDo);
                }
                else
                {
                    response = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public async Task<List<BO_InvoiceHeader>> UC_301_005_GetAllInvoicesHeadersAsync()
        {
            List<BO_InvoiceHeader> response = new();

            try
            {
                List<DO_InvoiceHeader> invoiceHeadersDo = await _headerRepository.GetInvoiceHeadersAsync();

                if (invoiceHeadersDo.Count > 0)
                {
                    response = _mapper.Map<List<DO_InvoiceHeader>, List<BO_InvoiceHeader>>(invoiceHeadersDo);
                }
                else
                {
                    response = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        // TODO implement this and it probably should give a response
        public async Task SaveInvoiceExceptionAsync(FrameworkException exception)
        {
            DO_InvoiceException invoiceException = _mapper.Map<DO_InvoiceException>(exception);

            await _exceptionRepository.SaveInvoiceExceptionAsync(invoiceException);
        }

        // TODO implement?
        private T HandleException<T>(T problemChild, Exception ex) where T : BaseResponse
        {
            T result;

            if (ex.InnerException != null)
            {
                result = HandleException<T>(problemChild, ex.InnerException);
            }
            else
            {
                problemChild.SetErrors(new ErrorResponse("none", ex.Message));
                result = problemChild;
            }

            return result;
        }
    }
}