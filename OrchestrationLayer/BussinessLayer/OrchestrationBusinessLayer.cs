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

        public async Task<InvoiceDetailResponse> UC_301_001_CreateInvoiceHeaderAsync(CreateInvoiceHeaderInput input)
        {
            InvoiceDetailResponse result = await _invoiceService.UC_301_001_CreateInvoiceHeaderAsync(input);
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

        public async Task<ArchiveInvoiceJournalEntryInput> UC_301_004_ArchiveJournalEntryForInvoice(ArchiveInvoiceJournalEntryInput input)
        {
            return await _invoiceService.UC_301_004_ArchiveJournalEntryForInvoiceAsync(input);
        }

        public async Task<CustomerDetailResponse> UC_200_002_SaveInvoiceForCustomer(CreateInvoiceInput invoice)
        {
            try
            {
                CustomerDetailResponse customer = await _customerService.UC_300_003_GetCustomerByIdAsync(invoice.ProxyId);

                if (customer.Company != null)
                {
                    InvoiceDetailResponse toCreate = await _invoiceService.UC_301_001_CreateInvoiceHeaderAsync(new(invoice.VatNumber, customer.Company.Id));

                    if (toCreate != null && toCreate.Success == true)
                    {
                        foreach (CreateInvoiceLine invoiceLine in invoice.InvoiceLines)
                        {
                            invoiceLine.InvoiceHeaderId = toCreate.Id;

                            InvoiceResponse res = await _invoiceService.UC_301_002_AddInvoiceLineToHeaderAsync(invoiceLine);

                            if (res.Success == false)
                            {
                                customer.Errors.AddRange(res.Errors);
                                customer.Success = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (toCreate.Success == false)
                        {
                            toCreate.Success = false;
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