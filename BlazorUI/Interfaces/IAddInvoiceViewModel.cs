using BlazorUI.Models.Input;
using BlazorUI.Models.Response;

namespace BlazorUI.Interfaces
{
    public interface IAddInvoiceViewModel
    {
        Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput invoiceInput);

        Task<CustomerDetailResponse> GetCustomerAsync(string guid);
    }
}