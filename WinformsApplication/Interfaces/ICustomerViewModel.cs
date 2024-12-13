using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Interfaces
{
    public interface ICustomerViewModel
    {
        CustomerResponse SelectedCustomer { get; set; }
        List<CustomerResponse> ListCustomers { get; set; }

        Task GetCustomersAsync();
    }
}