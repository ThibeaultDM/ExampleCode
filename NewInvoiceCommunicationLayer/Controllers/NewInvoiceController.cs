using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewInvoiceBusinessLayer.Enums;
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
        public async Task<IActionResult> UC_301_001_CreateInvoiceHeader(CreateInvoiceHeaderInput input)
        {
            ObjectResult response;
            CreateInvoiceHeaderResponse result = new();
            try
            {
                // Calls use case to create invoice header using input VAT number and company ID.
                BO_InvoiceHeader invoiceHeaderBo = await _invoiceUseCases.UC_301_001_CreateInvoiceHeaderAsync(input.VATNumber, input.ProxyCompanyId);

                // Maps business object to response model.
                result = _mapper.Map<CreateInvoiceHeaderResponse>(invoiceHeaderBo);

                // Adds any business rule errors to the response model.
                SetErrorMessage(result, invoiceHeaderBo);
            }
            catch (Exception ex)
            {
                // Returns error response on exception.
                result = HandleException(result, ex);
            }

            response = Ok(result);

            return response;
        }

        [HttpPatch("UC_301_002_AddInvoiceLineToHeader")]
        public async Task<IActionResult> UC_301_002_AddInvoiceLineToHeader(AddInvoiceLineToInvoiceHeaderInput input)
        {
            ObjectResult response;
            AddInvoiceLineToInvoiceHeaderResponse result = new();

            try
            {
                // Maps input data to the invoice line business object and adds it to the invoice header.
                BO_InvoiceLine invoiceLineBO = _mapper.Map<BO_InvoiceLine>(input);
                BO_InvoiceHeader invoiceHeaderBO = await _invoiceUseCases.UC_301_002_AddInvoiceLineToHeaderAsync(invoiceLineBO);

                result = MappingDetailedInvoiceHeaderResponse(invoiceHeaderBO);
                response = Ok(result);
            }
            catch (Exception ex)
            {
                // Returns error response on exception.
                result = HandleException(result, ex);
            }

            response = Ok(result);

            return response;
        }

        [HttpGet("UC_301_003_GetInvoiceByName/{Id}")]
        public async Task<IActionResult> UC_301_003_GetInvoiceByName(Guid Id)
        {
            ObjectResult response;
            AddInvoiceLineToInvoiceHeaderResponse result = new();

            try
            {
                // Calls use case to find invoice by ID.
                BO_InvoiceHeader invoiceHeaderBO = await _invoiceUseCases.UC_301_003_FindInvoiceHeaderAsync(Id);

                result = MappingDetailedInvoiceHeaderResponse(invoiceHeaderBO);
                response = Ok(result);
            }
            catch (Exception ex)
            {
                // Returns error response on exception.
                result = HandleException(result, ex);
            }

            response = Ok(result);

            return response;
        }

        [HttpGet("UC_301_005_GetAllInvoices")]
        public async Task<IActionResult> UC_301_005_GetAllInvoices()
        {
            ObjectResult response;

            try
            {
                // Retrieves all invoice headers.
                List<BO_InvoiceHeader> invoiceHeaderBOs = await _invoiceUseCases.UC_301_005_GetAllInvoicesHeadersAsync();
                List<AddInvoiceLineToInvoiceHeaderResponse> result = new();

                if (invoiceHeaderBOs.Count > 0)
                {
                    // Maps each invoice header to response and returns list.
                    result = invoiceHeaderBOs.Select(ih => MappingDetailedInvoiceHeaderResponse(ih)).ToList();
                }
                else
                {
                    // Creates empty response if no headers found.
                    AddInvoiceLineToInvoiceHeaderResponse toAdd = new();
                    toAdd.SetErrors(new(null, "No InvoiceHeaders Available"));

                    result.Add(toAdd);
                }
                response = Ok(result);
            }
            catch (Exception ex)
            {
                AddInvoiceLineToInvoiceHeaderResponse result = new();
                // Returns error response on exception.
                result = HandleException(result, ex);

                response = Ok(result);
            }

            return response;
        }

        [HttpPost("UC_301_004_ArchiveJournalEntryForInvoice")]
        public async Task<IActionResult> UC_301_004_ArchiveJournalEntryForInvoice(ArchiveInvoiceJournalEntry input)
        {
            ObjectResult response;

            try
            {
                // Calls use case to archive journal entry associated with invoice.
                BO_JournalEntry journalEntryBO = await _invoiceUseCases.UC_301_004_ArchiveJournalEntryForInvoiceAsync(input.JournalHeaderId, input.InvoiceHeaderId);
                ArchiveInvoiceJournalEntry result = _mapper.Map<ArchiveInvoiceJournalEntry>(journalEntryBO);

                SetErrorMessage(result, journalEntryBO);

                response = Ok(result);
            }
            catch (Exception ex)
            {
                BaseResponse errorResponse = new BaseResponse();
                response = Ok(HandleException(errorResponse, ex));
            }

            return response;
        }

        #region Helper Methodes

        // Adds errors from business object to response model if there are any broken rules.
        private static void SetErrorMessage<T>(T result, BusinessObjectBase BusinessObject) where T : BaseResponse
        {
            if (BusinessObject.BrokenRules.Count > 0)
            {
                BusinessObject.BrokenRules.ForEach(br => result.SetErrors(new(br.PropertyName, br.FailedMessage)));
            }
        }

        // Maps invoice header details to response model, including lines and errors if any.
        private AddInvoiceLineToInvoiceHeaderResponse MappingDetailedInvoiceHeaderResponse(BO_InvoiceHeader invoiceHeaderBO)
        {
            AddInvoiceLineToInvoiceHeaderResponse result = _mapper.Map<AddInvoiceLineToInvoiceHeaderResponse>(invoiceHeaderBO);
            SetErrorMessage(result, invoiceHeaderBO);

            if (invoiceHeaderBO.InvoiceLines != null && invoiceHeaderBO.InvoiceLines.Count > 0)
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

        // Handles exceptions by recursively logging inner exceptions and adding error messages to response.
        private T HandleException<T>(T result, Exception ex) where T : BaseResponse
        {
            if (ex.InnerException != null)
                return HandleException(result, ex.InnerException);
            else
            {
                string message;
                switch (ex.Message)
                {
                    case "Unrecognized Guid format":
                        message = $"{EnumDescription.GetDescription(InvoiceExceptionTypes.NotGuid)}";
                        break;

                    case "The UPDATE statement conflicted with the FOREIGN KEY constraint \"FK_InvoiceHeaders_Company\". The conflict occurred in database \"QueasoTraining\", table \"dbo.Companies\", column 'Id'.":
                    case "The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_InvoiceHeaders_Company\". The conflict occurred in database \"QueasoTraining\", table \"dbo.Companies\", column 'Id'.\r\nThe statement has been terminated.":
                        message = $"{EnumDescription.GetDescription(InvoiceExceptionTypes.CompanyNotFound)}";
                        break;
                    // Unknown Errors
                    default:
                        message = ex.Message;
                        break;
                }

                result.SetErrors(new("none", message));
            }

            return result;
        }

        #endregion Helper Methodes
    }
}