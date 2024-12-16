using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Controllers;
public interface ICustomerController
{
    CustomerController.AddInvoiceDelegate AddInvoiceAction { get; }
    List<CustomerResponse> Customers { get; set; }
    CustomerResponse SelectedCustomer { get; set; }

    event CustomerController.DataLoadedDelegate DataLoad;
}