using Microsoft.AspNetCore.Mvc;
using NewInvoiceCommunicationLayer.Interfaces;
using NewInvoiceCommunicationLayer.Models.Input;
using NewInvoiceCommunicationLayer.Models.Response;
using NewInvoiceServiceLayer.Objects;

namespace NewInvoiceCommunicationLayer.Controllers
{
    [ApiController]
    [Route("api/Invoice")]
    public class NewInvoiceController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IInvoiceUseCases _invoiceUseCases;

        public NewInvoiceController(IConfiguration config, IInvoiceUseCases invoiceUseCases)
        {
            _config = config;
            _invoiceUseCases = invoiceUseCases;
        }

        [HttpPost("CreateInvoiceHeader")]
        public async Task<IActionResult> CreateInvoiceHeaderAsync(CreateInvoiceHeaderInput input)
        {
            ObjectResult response;

            try
            {
                CreateInvoiceHeaderResponse result = await _invoiceUseCases.UC_301_001_CreateInvoiceHeaderAsync(input);

                if (result.Success)
                {
                    response = Ok(result);
                }
                else
                {
                    response = BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.InnerException);
            }

            return response;
        }

        [HttpPost("AddInvoiceLineToInvoiceHeader")]
        public async Task<IActionResult> AddInvoiceLineToInvoiceHeaderAsync(AddInvoiceLineToInvoiceHeaderInput input)
        {
            ObjectResult response;

            try
            {
                BO_InvoiceHeader result = await _invoiceUseCases.UC_301_002_AddInvoiceLineToHeaderAsync(input);

                if (result != null)
                {
                    response = Ok(result);
                }
                else
                {
                    response = BadRequest("InvoiceHeader not Found");
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }

            return response;
        }

        [HttpPost("GetInvoiceById")]
        public async Task<IActionResult> FindInvoiceByIdAsync(GetInvoiceByNameInput input)
        {
            ObjectResult response;

            try
            {
                BO_InvoiceHeader result = await _invoiceUseCases.UC_301_003_FindInvoiceHeaderAsync(input);

                if (result != null)
                {
                    response = Ok(result);
                }
                else
                {
                    response = BadRequest("InvoiceHeader not Found");
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.InnerException);
            }

            return response;
        }

        [HttpGet("GetAllInvoices")]
        public async Task<IActionResult> GetAllInvoicesAsync()
        {
            ObjectResult response;

            try
            {
                List<BO_InvoiceHeader> result = await _invoiceUseCases.UC_301_005_GetAllInvoicesHeadersAsync();

                if (result != null)
                {
                    response = Ok(result);
                }
                else
                {
                    response = BadRequest("No invoiceHeaders in database");
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.InnerException);
            }

            return response;
        }

        [HttpPost("ArchiveInvoiceJournalEntry")]
        public async Task<IActionResult> ArchiveInvoiceJournalEntryAsync(ArchiveInvoiceJournalEntryInput input)
        {
            ObjectResult response;

            try
            {
                BO_InvoiceHeader result = await _invoiceUseCases.UC_301_004_ArchiveJournalEntryForInvoiceAsync(input);

                if (result != null)
                {
                    response = Ok(result);
                }
                else
                {
                    response = BadRequest("InvoiceHeader not Found");
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.InnerException);
            }

            return response;
        }
    }
}