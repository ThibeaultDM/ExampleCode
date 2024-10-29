using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewInvoiceBusinessLayer.Objects;
using NewInvoiceCommunicationLayer.Models.Input;
using NewInvoiceCommunicationLayer.Models.Response;
using NewInvoiceServiceLayer.Interfaces;
using NewInvoiceServiceLayer.Objects;
using QueasoFramework.BusinessModels;

namespace NewInvoiceCommunicationLayer.Controllers
{
    [ApiController]
    [Route("api/Invoice")]
    public class NewInvoiceController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IInvoiceUseCases _invoiceUseCases;
        private readonly IMapper _mapper;

        public NewInvoiceController(IConfiguration config, IInvoiceUseCases invoiceUseCases, IMapper mapper)
        {
            this._config = config;
            this._invoiceUseCases = invoiceUseCases;
            this._mapper = mapper;
        }

        [HttpPost("UC_301_001_CreateInvoiceHeader")]
        public async Task<IActionResult> CreateInvoiceHeaderAsync(CreateInvoiceHeaderInput input)
        {
            ObjectResult response;
            try
            {
                BO_InvoiceHeader invoiceHeaderBo = await _invoiceUseCases.UC_301_001_CreateInvoiceHeaderAsync(input.VATNumber, input.ProxyCompanyId);

                CreateInvoiceHeaderResponse result = _mapper.Map<CreateInvoiceHeaderResponse>(invoiceHeaderBo);

                SetErrorMessage(result, invoiceHeaderBo);

                response = Ok(result);
            }
            catch (Exception ex)
            {
                response = BadRequest(HandleException(ex));
            }

            return response;
        }

        [HttpPost("UC_301_002_AddInvoiceLineToHeader")]
        public async Task<IActionResult> AddInvoiceLineToInvoiceHeaderAsync(AddInvoiceLineToInvoiceHeaderInput input)
        {
            ObjectResult response;

            try
            {
                BO_InvoiceLine invoiceLineBO = _mapper.Map<BO_InvoiceLine>(input);
                BO_InvoiceHeader invoiceHeaderBO = await _invoiceUseCases.UC_301_002_AddInvoiceLineToHeaderAsync(invoiceLineBO);

                response = CheckIfHeaderBOIsValidAndGiveResponse(invoiceHeaderBO);
            }
            catch (Exception ex)
            {
                response = BadRequest(HandleException(ex));
            }

            return response;
        }

        [HttpPost("UC_301_003_FindInvoiceHeader")]
        public async Task<IActionResult> FindInvoiceByIdAsync(GetInvoiceByNameInput input)
        {
            ObjectResult response;

            try
            {
                BO_InvoiceHeader invoiceHeaderBO = await _invoiceUseCases.UC_301_003_FindInvoiceHeaderAsync(input.InvoiceHeaderId);

                response = CheckIfHeaderBOIsValidAndGiveResponse(invoiceHeaderBO);
            }
            catch (Exception ex)
            {
                response = BadRequest(HandleException(ex));
            }

            return response;
        }

        [HttpPost("UC_301_005_GetAllInvoicesHeaders")]
        public async Task<IActionResult> GetAllInvoicesAsync()
        {
            ObjectResult response;

            try
            {
                List<BO_InvoiceHeader> invoiceHeaderBOs = await _invoiceUseCases.UC_301_005_GetAllInvoicesHeadersAsync();

                if (invoiceHeaderBOs.Count > 0)
                {
                    List<AddInvoiceLineToInvoiceHeaderResponse> result = invoiceHeaderBOs.Select(ih => MappingDetailedInvoiceHeaderResponse(ih)).ToList();

                    response = Ok(result);
                }
                else
                {
                    BaseResponse result = new();
                    result.SetErrors(new(null, "No InvoiceHeaders Available"));

                    response = Ok(result);
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(HandleException(ex));
            }

            return response;
        }

        [HttpPost("UC_301_004_ArchiveJournalEntryForInvoice")]
        public async Task<IActionResult> ArchiveInvoiceJournalEntryAsync(ArchiveInvoiceJournalEntry input)
        {
            ObjectResult response;

            try
            {
                BO_JournalEntry journalEntryBO = await _invoiceUseCases.UC_301_004_ArchiveJournalEntryForInvoiceAsync(input.JournalHeaderId, input.InvoiceHeaderId);

                ArchiveInvoiceJournalEntry result = _mapper.Map<ArchiveInvoiceJournalEntry>(journalEntryBO);
                response = Ok(result);
            }
            catch (Exception ex)
            {
                response = BadRequest(HandleException(ex));
            }

            return response;
        }

        #region Helper Methodes

        private static void SetErrorMessage<T>(T result, BusinessObjectBase BusinessObject) where T : BaseResponse
        {
            if (BusinessObject.BrokenRules.Count > 0)
            {
                BusinessObject.BrokenRules.ForEach(br => result.SetErrors(new(br.PropertyName, br.FailedMessage)));
            }
        }

        private AddInvoiceLineToInvoiceHeaderResponse MappingDetailedInvoiceHeaderResponse(BO_InvoiceHeader invoiceHeaderBO)
        {
            AddInvoiceLineToInvoiceHeaderResponse result = _mapper.Map<AddInvoiceLineToInvoiceHeaderResponse>(invoiceHeaderBO);
            SetErrorMessage(result, invoiceHeaderBO);

            if (invoiceHeaderBO.InvoiceLines.Count > 0)
            {
                InvoiceLineResponse invoiceLineResponse = new();
                foreach (BO_InvoiceLine invoiceLine in invoiceHeaderBO.InvoiceLines)
                {
                    invoiceLineResponse = _mapper.Map<InvoiceLineResponse>(invoiceLine);
                    SetErrorMessage(invoiceLineResponse, invoiceLine);
                }
            }
            else
            {
                result.InvoiceLines = null;
            }

            return result;
        }

        private BaseResponse HandleException(Exception ex)
        {
            BaseResponse result = new();

            if (ex.InnerException != null)
                return HandleException(ex.InnerException);
            else
            {
                result.SetErrors(new("none", ex.Message));
            }

            return result;
        }

        private ObjectResult CheckIfHeaderBOIsValidAndGiveResponse(BO_InvoiceHeader invoiceHeaderBO)
        {
            ObjectResult response;

            if (invoiceHeaderBO.BrokenRules.Count == 0)
            {
                AddInvoiceLineToInvoiceHeaderResponse result = MappingDetailedInvoiceHeaderResponse(invoiceHeaderBO);

                response = Ok(result);
            }
            else
            {
                BaseResponse result = new();
                SetErrorMessage(result, invoiceHeaderBO);

                response = Ok(result);
            }

            return response;
        }

        #endregion Helper Methodes
    }
}