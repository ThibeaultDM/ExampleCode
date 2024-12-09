using Orchestration.Models.Input;
using Orchestration.Models.Response;

namespace Orchestration.Interfaces
{
    public interface IOrchestrationService
    {
        #region Invoice

        Task<List<InvoiceResponse>> GetAllInvoicesAsync();

        Task<InvoiceDetailResponse> UC_301_001_CreateInvoiceHeaderAsync(CreateInvoiceHeaderInput input);

        Task<InvoiceResponse> UC_301_002_AddInvoiceLineToHeaderAsync(CreateInvoiceLine invoiceLineInput);

        Task<InvoiceDetailResponse> UC_301_003_GetInvoiceByNameAsync(Guid invoiceId);

        Task<ArchiveInvoiceJournalEntryResponse> UC_301_004_ArchiveJournalEntryForInvoice(ArchiveInvoiceJournalEntry input);

        #endregion Invoice

        #region Customer

        Task<CustomerDetailResponse> UC_300_001_CreateCustomerAsync(CreateCustomerInput customerToCreate);

        Task<CustomerDetailResponse> UC_300_002_GetCustomerByIdAsync(Guid customerId);

        Task<List<CustomerResponse>> UC_300_003_GetAllCustomersAsync();

        #endregion Customer

        #region Combined

        Task<CustomerDetailResponse> UC_200_002_SaveInvoiceForCustomer(CreateInvoiceInput invoice);

        #endregion Combined
    }
}