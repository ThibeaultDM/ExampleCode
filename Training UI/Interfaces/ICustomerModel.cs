using Training_UI.Models.Response;

namespace Training_UI.Interfaces
{
    public interface ICustomerModel
    {
        List<CustomerResponse> Customers { get; }

        Task FetchAllCustomersAsync();
    }
}