using Training_UI.Models.Response;

namespace Training_UI.Interfaces
{
    public interface ICustomerViewModel
    {
        List<CustomerResponse> ListCustomers { get; set; }

        Task GetCustomersAsync();
    }
}