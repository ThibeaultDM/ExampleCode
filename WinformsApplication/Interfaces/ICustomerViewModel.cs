using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Interfaces
{
    public interface ICustomerViewModel
    {
        List<CustomerResponse> ListCustomers { get; set; }

        Task GetCustomersAsync();
    }
}