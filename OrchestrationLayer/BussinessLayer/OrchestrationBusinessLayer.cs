using AutoMapper;
using Orchestration.Interfaces;
using Orchestration.Models.Input;
using Orchestration.Models.Response;

namespace Orchestration.BusinessLayer
{
    public class OrchestrationBusinessLayer : IOrchestrationService
    {
        private readonly IMapper _mapper;
        private ICustomerService _customerService;
        private IInvoiceService _invoiceService;

        public OrchestrationBusinessLayer(IMapper mapper, ICustomerService customerService, IInvoiceService invoiceService)
        {
            _mapper = mapper;
            _customerService = customerService;
            _invoiceService = invoiceService;
        }

        #region Invoice

        public async Task<List<InvoiceResponse>> GetAllInvoicesAsync()
        {
            List<InvoiceResponse> result = await _invoiceService.UC_301_005_GetAllInvoicesAsync();
            return result;
        }

        public async Task<InvoiceDetailResponse> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber)
        {
            InvoiceDetailResponse result = await _invoiceService.UC_301_001_CreateInvoiceHeaderAsync(vatNumber);
            return result;
        }

        public async Task<InvoiceResponse> UC_301_002_AddInvoiceLineToHeaderAsync(CreateInvoiceLine invoiceLineInput)
        {
            InvoiceResponse result = await _invoiceService.UC_301_002_AddInvoiceLineToHeaderAsync(invoiceLineInput);
            return result;
        }

        public async Task<InvoiceDetailResponse> UC_301_003_GetInvoiceByNameAsync(Guid invoiceId)
        {
            InvoiceDetailResponse result = await _invoiceService.UC_301_003_GetInvoiceByNameAsync(invoiceId);
            return result;
        }

        #endregion Invoice

        #region Customer

        public async Task<CustomerDetailResponse> UC_300_001_CreateCustomerAsync(CreateCustomerInput customerToCreate)
        {
            CustomerDetailResponse result = await _customerService.UC_300_001_CreateCustomerAsync(customerToCreate);
            return result;
        }

        public async Task<CustomerDetailResponse> UC_300_002_GetCustomerByIdAsync(Guid customerId)
        {
            CustomerDetailResponse result = await _customerService.UC_300_003_GetCustomerByIdAsync(customerId);
            return result;
        }

        public async Task<List<CustomerResponse>> UC_300_003_GetAllCustomersAsync()
        {
            List<CustomerResponse> result = await _customerService.UC_300_002_GetAllCustomerAsync();
            return result;
        }

        #endregion Customer

        #region Combined

        public Task<List<CustomerResponse>> UC_200_002_SaveInvoiceForCustomerAsync(CreateCustomerInput customerToCreate, CreateInvoiceInput invoiceHeaderInput)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDetailResponse> UC_300_004_ArchiveCustomerInvoiceAsync(CreateInvoiceInput invoice)
        {
            try
            {
                CustomerDetailResponse customer = await _customerService.UC_300_003_GetCustomerByIdAsync(invoice.ProxyId);

                if (customer.Company != null)
                {
                    InvoiceDetailResponse toCreate = await _invoiceService.UC_301_001_CreateInvoiceHeaderAsync(invoice.VatNumber);

                    if (toCreate != null && toCreate.Succes == true)
                    {
                        foreach (CreateInvoiceLine invoiceLine in invoice.InvoiceLines)
                        {
                            // todo ask why are you mapping this?
                            //var line = _mapper.Map<CreateInvoiceLine>(invoiceLine);
                            invoiceLine.InvoiceHeaderId = toCreate.Id;

                            var res = await _invoiceService.UC_301_002_AddInvoiceLineToHeaderAsync(invoiceLine);

                            if (res.Succes == false)
                            {
                                customer.Errors.AddRange(res.Errors);
                                customer.Succes = false;
                                break;
                            }
                        }

                        // link header id to invoice id
                        ArchiveInvoiceJournalEntryInput archiveInvoice = new(toCreate.Id, invoice.ProxyId);
                        await _invoiceService.UC_301_004_ArchiveJournalEntryForInvoiceAsync(archiveInvoice);
                    }
                    else
                    {
                        if (toCreate.Succes == false)
                        {
                            toCreate.Succes = false;
                            customer.Errors.AddRange(toCreate.Errors);
                        }
                    }
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Combined
    }
}