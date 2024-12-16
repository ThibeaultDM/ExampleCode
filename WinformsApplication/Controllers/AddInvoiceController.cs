using WinFormsApplication.Interfaces;
using WinFormsApplication.Models.Input;
using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Controllers;

public class AddInvoiceController : IAddInvoiceController
{
    private IDataModel invoiceModel;
    private CustomerDetailResponse _customer;
    private string _idCustomer;
    private List<ErrorResponse> _errors;

    public AddInvoiceController(IDataModel customerModel)
    {
        invoiceModel = customerModel;
    }
    public delegate void CustomerSearch();
    public delegate void InvoiceErrors();

    public event CustomerSearch CustomerSearchCompleted;
    public event InvoiceErrors ThereIsAProblem; // No it isn't me 

    public CreateInvoiceInput InvoiceToCreate { get; set; } = new();

    public string IdCustomer
    {
        get => _idCustomer;
        set
        {
            _idCustomer = value;
            GetCustomerAsync(value);
        }
    }

    public CustomerDetailResponse Customer
    {
        get => _customer;
        set
        {
            _customer = value;
            CustomerSearchCompleted.Invoke();
        }
    }

    public List<ErrorResponse> Errors
    {
        get => _errors;
        set
        {
            _errors = value;
            ThereIsAProblem.Invoke();
        }
    }

    public async Task GetCustomerAsync(string searchId)
    {
        Customer = await invoiceModel.GetCustomerAsync(searchId);

        InvoiceToCreate.ProxyId = Customer.Company.Id;
        if (Customer.Errors.Count > 0)
        {
            Errors = Customer.Errors;
        }
    }

    public async Task CreateInvoiceAsync()
    {
        Customer = await invoiceModel.CreateInvoiceAsync(InvoiceToCreate);

        if (Customer.Errors.Count > 0)
        {
            Errors = Customer.Errors;
        }
    }
}