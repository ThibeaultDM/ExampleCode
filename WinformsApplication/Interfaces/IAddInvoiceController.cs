using WinFormsApplication.Controllers;
using WinFormsApplication.Models.Input;
using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Interfaces;
public interface IAddInvoiceController
{
    CustomerDetailResponse Customer { get; set; }
    List<ErrorResponse> Errors { get; set; }
    string IdCustomer { get; set; }
    CreateInvoiceInput InvoiceToCreate { get; set; }

    event AddInvoiceController.CustomerSearch CustomerSearchCompleted;
    event AddInvoiceController.InvoiceErrors ThereIsAProblem;

    Task CreateInvoiceAsync();
    Task GetCustomerAsync(string searchId);
}