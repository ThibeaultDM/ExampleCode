using Microsoft.Extensions.DependencyInjection;
using WinFormsApplication.Interfaces;
using WinFormsApplication.Models.Response;

namespace BlazorUI.ViewModels;

public class CustomerController : ICustomerController
{
    private IDataModel _customerModel;
    private readonly IServiceProvider _serviceProvider;

    public CustomerController(IDataModel customerModel, IServiceProvider serviceProvider)
    {
        Console.WriteLine("CustomerViewModel constructor working");
        _customerModel = customerModel;
        _serviceProvider = serviceProvider;

        AddInvoiceAction = ExecuteAddInvoice;
    }

    public List<CustomerResponse> ListCustomers { get; set; }
    public CustomerResponse SelectedCustomer { get; set; }
    public AddInvoiceDelegate AddInvoiceAction { get; set; }

    public async Task GetCustomersAsync()
    {
        await _customerModel.GetAllCustomersAsync();

        ListCustomers = _customerModel.Customers;

        Console.WriteLine("FetchDataViewModel Customer ");
    }

    public delegate void AddInvoiceDelegate();

    public void ExecuteAddInvoice()
    {
        // todo look for memory leak, the container will not explicitly dispose of manually instantiated types, even if they implement IDisposable
        IAddInvoiceView addInvoice = _serviceProvider.GetRequiredService<IAddInvoiceView>();
        addInvoice.CustomerId = SelectedCustomer.Id;
        addInvoice.Show();
    }
}