using Microsoft.Extensions.DependencyInjection;
using WinFormsApplication.Interfaces;
using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Controllers;

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
        _ = GetCustomersAsync();
    }

    public delegate void DataLoadedDelegate();
    public delegate void AddInvoiceDelegate();

    public event DataLoadedDelegate DataLoad;
    public AddInvoiceDelegate AddInvoiceAction { get; private set; }
    public CustomerResponse SelectedCustomer { get; set; }
    public List<CustomerResponse> Customers { get; set; }
    private async Task GetCustomersAsync()
    {
        Console.WriteLine("FetchDataViewModel Customer ");

        await _customerModel.GetAllCustomersAsync();
        Customers = _customerModel.Customers;
        DataLoad.Invoke();
    }

    private void ExecuteAddInvoice()
    {
        IAddInvoiceView addInvoice = _serviceProvider.GetRequiredService<IAddInvoiceView>();
        addInvoice.CustomerId = SelectedCustomer.Id;
        addInvoice.Show();
    }
}
