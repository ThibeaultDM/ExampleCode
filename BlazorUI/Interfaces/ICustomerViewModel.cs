using BlazorUI.Models.Response;

namespace BlazorUI.Interfaces;

public interface ICustomerViewModel
{
    List<CustomerResponse> ListCustomers { get; set; }

    Task GetCustomersAsync();
}