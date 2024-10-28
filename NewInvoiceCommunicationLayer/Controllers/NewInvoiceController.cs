using Microsoft.AspNetCore.Mvc;
using NewInvoiceServiceLayer.Interfaces;
using NewInvoiceCommunicationLayer.Models.Input;
using NewInvoiceCommunicationLayer.Models.Response;
using NewInvoiceServiceLayer.Objects;
using AutoMapper;

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

        [HttpPost("CreateInvoiceHeader")]
        public async Task<IActionResult> CreateInvoiceHeaderAsync(CreateInvoiceHeaderInput input)
        {
            ObjectResult response;
            try
            {
                BO_InvoiceHeader invoiceHeaderBo = await _invoiceUseCases.UC_301_001_CreateInvoiceHeaderAsync(input.VATNumber);

                CreateInvoiceHeaderResponse result = _mapper.Map<CreateInvoiceHeaderResponse>(invoiceHeaderBo);

                SetErrorMessage(result, invoiceHeaderBo);

                response = Ok(result);

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
                BO_InvoiceLine invoiceLineBO = _mapper.Map<BO_InvoiceLine>(input);
                BO_InvoiceHeader result = await _invoiceUseCases.UC_301_002_AddInvoiceLineToHeaderAsync(invoiceLineBO);

                response = Ok(result);
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
                BO_InvoiceHeader result = await _invoiceUseCases.UC_301_003_FindInvoiceHeaderAsync(input.InvoiceHeaderId);

                if (result != null)
                {
                    response = Ok(result);
                }
                else
                {
                    response = Ok("InvoiceHeader not Found");
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

                if (result.Count > 0)
                {
                    response = Ok(result);
                }
                else
                {
                    response = Ok("No invoiceHeaders in database");
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
                BO_InvoiceHeader result = await _invoiceUseCases.UC_301_004_ArchiveJournalEntryForInvoiceAsync(input.proxyCompanyId, input.InvoiceHeaderId);

                if (result != null)
                {
                    response = Ok(result);
                }
                else
                {
                    response = Ok("InvoiceHeader not Found");
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.InnerException);
            }

            return response;
        }

        private static void SetErrorMessage<T>(T result, BO_InvoiceHeader invoiceHeaderBo) where T : BaseResponse
        {
            if (invoiceHeaderBo.BrokenRules.Count > 0)
            {
                invoiceHeaderBo.BrokenRules.ForEach(br => result.SetErrors(new(br.PropertyName, br.FailedMessage)));

            }
        }

    }
}