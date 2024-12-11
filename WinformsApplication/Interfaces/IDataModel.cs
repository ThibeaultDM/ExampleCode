using WinformsApplication.Models.Input;
using WinformsApplication.Models.Response;

namespace WinformsApplication.Interfaces
{
    public interface IDataModel
    {
        List<CustomerResponse> Customers { get; }

        Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput createInvoice);

        Task GetAllCustomersAsync();

        Task<CustomerDetailResponse> GetCustomerAsync(string customerId);
    }
}