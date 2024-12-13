using BlazorUI.Interfaces;
using BlazorUI.Models.Response;

namespace BlazorUI.ViewModels;

public class CustomerViewModel : ICustomerViewModel
{
    private IDataModel customerModel;

    public CustomerViewModel(IDataModel customerModel)
    {
        Console.WriteLine("CustomerViewModel constructor working");
        this.customerModel = customerModel;
    }

    public List<CustomerResponse> ListCustomers { get; set; }

    public async Task GetCustomersAsync()
    {
        await customerModel.GetAllCustomersAsync();

        ListCustomers = customerModel.Customers;

        Console.WriteLine("FetchDataViewModel Customer ");
    }
}