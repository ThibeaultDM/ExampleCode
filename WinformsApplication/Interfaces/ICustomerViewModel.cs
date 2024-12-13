using WinFormsApplication.Models.Response;
using static BlazorUI.ViewModels.CustomerViewModel;

namespace WinFormsApplication.Interfaces
{
    public interface ICustomerViewModel
    {
        CustomerResponse SelectedCustomer { get; set; }
        List<CustomerResponse> ListCustomers { get; set; }
        AddInvoiceDelegate AddInvoiceAction { get; set; }
        Task GetCustomersAsync();
    }
}