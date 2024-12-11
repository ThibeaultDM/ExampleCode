using WinformsApplication.Models.Response;

namespace WinformsApplication.Interfaces
{
    public interface ICustomerViewModel
    {
        List<CustomerResponse> ListCustomers { get; set; }

        Task GetCustomersAsync();
    }
}