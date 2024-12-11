using WinformsApplication.Models.Input;
using WinformsApplication.Models.Response;

namespace WinformsApplication.Interfaces
{
    public interface IAddInvoiceViewModel
    {
        Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput invoiceInput);

        Task<CustomerDetailResponse> GetCustomerAsync(string searchId);
    }
}