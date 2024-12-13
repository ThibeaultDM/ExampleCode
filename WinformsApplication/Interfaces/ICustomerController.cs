using WinFormsApplication.Models.Response;
using static BlazorUI.ViewModels.CustomerController;

namespace WinFormsApplication.Interfaces;

public interface ICustomerController
{
    CustomerResponse SelectedCustomer { get; set; }
    List<CustomerResponse> ListCustomers { get; set; }
    AddInvoiceDelegate AddInvoiceAction { get; set; }
    Task GetCustomersAsync();
}