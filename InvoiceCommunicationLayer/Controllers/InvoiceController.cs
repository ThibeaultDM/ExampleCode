using AutoMapper;
using InvoiceBusinessLayer.BusinessObjects;
using InvoiceBusinessLayer.Interfaces;
using InvoiceCommunicationLayer.Models.Input;
using InvoiceCommunicationLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceCommunicationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IInvoiceUseCases _invoiceUseCases;
        private readonly IMapper _mapper;

        public InvoiceController(IConfiguration config, IInvoiceUseCases invoiceUseCases, IMapper mapper)
        {
            _config = config;
            _invoiceUseCases = invoiceUseCases;
            _mapper = mapper;
        }

        [HttpPost("CreateInvoiceHeader")]
        public async Task<IActionResult> CreateInvoiceHeaderAsync(CreateInvoiceHeaderInput createInvoiceHeaderInput)
        {
            CreateInvoiceHeaderResponse response = new();

            try
            {
                BO_InvoiceHeader result = await _invoiceUseCases.UC_301_001_CreateInvoiceHeaderAsync(createInvoiceHeaderInput.VATNumber);

                if (result.Valid)
                {
                    response = _mapper.Map<CreateInvoiceHeaderResponse>(result);
                    response.Succes = true;
                }
                else
                {
                    response.Succes = false;

                    foreach (var brokenRule in result.BrokenRules)
                    {
                        response.Errors.Add(new ErrorResponse()
                        {
                            ErrorMessage = brokenRule.FailedMessage,
                            PropertyName = brokenRule.PropertyName
                        });
                    }
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = HandleException(response, ex);
                throw;
            }
        }

        [HttpPost("AddInvoiceLineToInvoiceHeader")]
        public async Task<IActionResult> AddInvoiceLineToInvoiceHeaderAsync(AddInvoiceLineToInvoiceHeaderInput addInvoiceLineToInvoiceHeaderInput)
        {
            try
            {
                BO_InvoiceLine invoiceLineBo = new();

                invoiceLineBo.PricePerUnit = addInvoiceLineToInvoiceHeaderInput.PricePerUnit;
                invoiceLineBo.VATRate = addInvoiceLineToInvoiceHeaderInput.VATRate;
                invoiceLineBo.Quantity = addInvoiceLineToInvoiceHeaderInput.Quantity;
                invoiceLineBo.Description = addInvoiceLineToInvoiceHeaderInput.Description;

                if (invoiceLineBo.Valid != true)
                {
                    return BadRequest(invoiceLineBo.BrokenRules);
                }

                var result = await _invoiceUseCases.UC_301_002_AddInvoiceLineToHeaderAsync(addInvoiceLineToInvoiceHeaderInput.InvoiceHeaderId, invoiceLineBo);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("GetInvoiceById")]
        public async Task<IActionResult> GetInvoiceByIdAsync(GetInvoiceByNameInput getInvoiceByNameInput)
        {
            try
            {
                var result = await _invoiceUseCases.UC_301_003_GetInvoiceByNameAsync(getInvoiceByNameInput.InvoiceHeaderId);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Header not found");
            }
        }

        [HttpGet("GetAllInvoices")]
        public async Task<IActionResult> GetAllInvoicesAsync()
        {
            try
            {
                var result = await _invoiceUseCases.UC_301_005_GetAllInvoicesAsync();

                if (result.Count < 1)
                {
                    return Ok("there are no Invoices in the database");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("ArchiveInvoiceJournalEntry")]
        public async Task<IActionResult> ArchiveInvoiceJournalEntryAsync(ArchiveInvoiceJournalEntryInput archiveInvoiceJournalEntryInput)
        {
            CreateInvoiceHeaderResponse response = new();
            try
            {
                var result = await _invoiceUseCases.UC_301_004_ArchiveJournalEntryForInvoiceAsync(archiveInvoiceJournalEntryInput.JournalEntryId, archiveInvoiceJournalEntryInput.JournalHeaderId);

                response = _mapper.Map<CreateInvoiceHeaderResponse>(result);

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private T HandleException<T>(T response, Exception ex) where T : BaseResponse
        {
            if (ex.InnerException != null)
            {
                return HandleException<T>(response, ex.InnerException);
            }
            else
            {
                response.Errors.Add(new ErrorResponse()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = "none"
                });

                response.Succes = false;
                return response;
            }
        }
    }
}