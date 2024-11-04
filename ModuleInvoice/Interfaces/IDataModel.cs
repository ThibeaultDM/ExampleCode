using ModuleCustomer.Models.Response;

namespace ModuleInvoice.Interfaces
{
    public interface IDataModel
    {
        List<CustomerResponse> Customers { get; }

        Task GetAllCustomersAsync();

        Task<CustomerDetailResponse> GetCustomerAsync(string customerId);
    }
}