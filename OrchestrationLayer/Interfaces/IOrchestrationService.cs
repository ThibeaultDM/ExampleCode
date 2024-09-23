using Orchestration.Models.Input;
using Orchestration.Models.Response;

namespace Orchestration.Interfaces
{
    public interface IOrchestrationService
    {
        #region Invoice

        Task<List<InvoiceResponse>> GetAllInvoicesAsync();

        Task<InvoiceDetailResponse> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber);

        Task<InvoiceResponse> UC_301_002_AddInvoiceLineToHeaderAsync(CreateInvoiceLine invoiceLineInput);

        Task<InvoiceDetailResponse> UC_301_003_GetInvoiceByNameAsync(Guid invoiceId);

        #endregion Invoice

        #region Customer

        Task<CustomerDetailResponse> UC_300_001_CreateCustomerAsync(CreateCustomerInput customerToCreate);

        Task<CustomerDetailResponse> UC_300_002_GetCustomerByIdAsync(Guid customerId);

        Task<List<CustomerResponse>> UC_300_003_GetAllCustomersAsync();

        #endregion Customer

        #region Combined

        Task<CustomerDetailResponse> UC_300_004_ArchiveCustomerInvoiceAsync(CreateInvoiceInput invoice);

        Task<List<CustomerResponse>> UC_200_002_SaveInvoiceForCustomerAsync(CreateCustomerInput customerToCreate, CreateInvoiceInput invoiceHeaderInput);

        #endregion Combined
    }
}