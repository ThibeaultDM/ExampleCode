using Training_UI.Models.Input;
using Training_UI.Models.Response;

namespace Training_UI.Interfaces
{
    public interface IDataModel
    {
        List<CustomerResponse> Customers { get; }

        Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput createInvoice);

        Task GetAllCustomersAsync();

        Task<CustomerDetailResponse> GetCustomerAsync(string customerId);
    }
}