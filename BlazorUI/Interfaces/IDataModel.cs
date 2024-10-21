using BlazorUI.Models.Input;
using BlazorUI.Models.Response;

namespace BlazorUI.Interfaces
{
    public interface IDataModel
    {
        List<CustomerResponse> Customers { get; }

        Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput createInvoice);

        Task GetAllCustomersAsync();

        Task<CustomerDetailResponse> GetCustomerAsync(string customerId);
    }
}