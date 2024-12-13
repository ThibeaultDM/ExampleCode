using Microsoft.Extensions.DependencyInjection;
using WinFormsApplication.Interfaces;
using WinFormsApplication.Models.Response;

namespace BlazorUI.ViewModels
{
    public class CustomerViewModel : ICustomerViewModel
    {
        private IDataModel _customerModel;
        private readonly IServiceProvider _serviceProvider;
        private List<CustomerResponse> listCustomers;

        public CustomerViewModel(IDataModel customerModel, IServiceProvider serviceProvider)
        {
            Console.WriteLine("CustomerViewModel constructor working");
            this._customerModel = customerModel;
            this._serviceProvider = serviceProvider;

        }

        public List<CustomerResponse> ListCustomers { get => listCustomers; set => listCustomers = value; }
        public CustomerResponse SelectedCustomer { get; set; }

        public async Task GetCustomersAsync()
        {
            await _customerModel.GetAllCustomersAsync();

            ListCustomers = _customerModel.Customers;

            Console.WriteLine("FetchDataViewModel Customer ");
        }

        public delegate void AddInvoice();

        public void AddInvoice()
        {
            var addInvoice = _serviceProvider.GetRequiredService<IAddInvoiceView>();
            addInvoice.CustomerId = SelectedCustomer.Id;
            addInvoice.Show();

        }
    }
}