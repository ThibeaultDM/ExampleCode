﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orchestration.Interfaces;
using Orchestration.Models.Input;
using Orchestration.Models.Response;

namespace Orchestration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "OrchestrationService")]
    public class OrchestrationController : ControllerBase
    {
        private readonly IOrchestrationService service;
        private readonly IMapper _mapper;

        public OrchestrationController(IOrchestrationService service, IMapper mapper)
        {
            this.service = service;
            this._mapper = mapper;
        }

        #region Invoice

        [HttpGet("UC_301_006_GetAllInvoices")]
        public async Task<IActionResult> GetAllInvoices()
        {
            ObjectResult response;

            try
            {
                var result = await service.GetAllInvoicesAsync();

                response = Ok(result);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }

            return response;
        }

        [HttpPost("UC_301_003_GetInvoiceByName")]
        public async Task<IActionResult> GetInvoiceById(Guid invoiceId)
        {
            ObjectResult response;

            try
            {
                var result = await service.UC_301_003_GetInvoiceByNameAsync(invoiceId);

                response = Ok(result);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }

            return response;
        }

        [HttpPost("UC_301_001_CreateInvoiceHeaderAsync")]
        public async Task<IActionResult> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber)
        {
            ObjectResult response;

            try
            {
                var result = await service.UC_301_001_CreateInvoiceHeaderAsync(vatNumber);

                response = Ok(result);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        #endregion Invoice

        #region Customer

        [HttpPost("UC_300_001_CreateCustomerAsync")]
        public async Task<IActionResult> UC_300_001_CreateCustomerAsync(CreateCustomerInput customerToCreate)
        {
            ObjectResult response;
            try
            {
                var result = await service.UC_300_001_CreateCustomerAsync(customerToCreate);

                response = Ok(result);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [HttpGet("UC_300_002_GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var result = await service.UC_300_003_GetAllCustomersAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UC_300_003_GetCustomerByName")]
        public async Task<IActionResult> GetCustomerById(Guid customerId)
        {
            try
            {
                var result = await service.UC_300_002_GetCustomerByIdAsync(customerId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Customer

        #region Combined

        //[HttpPost("UC_301_009_GetJournalForEntry")]
        //public Task<List<InvoiceResponse>> GetJournalForEntry(Guid customerId)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpPost("UC_300_004_ArchiveCustomerInvoice")]
        public async Task<IActionResult> ArchiveCustomerInvoice(CreateInvoiceInput _invoice)
        {
            // todo ask if this is how it should work 1. creates a new customer for every invoice
            ObjectResult response;

            try
            {
                CustomerDetailResponse result = await service.UC_300_004_ArchiveCustomerInvoiceAsync(_invoice);

                response = Ok(result);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }

            return response;
        }

        #endregion Combined
    }
}