using Training_UI.Models.Input;
using Training_UI.Models.Response;

namespace Training_UI.Interfaces
{
    public interface IAddInvoiceViewModel
    {
        Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput invoiceInput);
        Task<CustomerDetailResponse> GetCustomerAsync(string guid);
    }
}