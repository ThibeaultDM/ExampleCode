using Microsoft.Extensions.DependencyInjection;
using WinFormsApplication.Interfaces;
using WinFormsApplication.Models.Response;

namespace BlazorUI.ViewModels
{
    public class CustomerController : ICustomerController
    {
        private IDataModel _customerModel;
        private readonly IServiceProvider _serviceProvider;
        private List<CustomerResponse> listCustomers;

        public CustomerController(IDataModel customerModel, IServiceProvider serviceProvider)
        {
            Console.WriteLine("CustomerViewModel constructor working");
            this._customerModel = customerModel;
            this._serviceProvider = serviceProvider;

            AddInvoiceAction = ExecuteAddInvoice;
        }

        public List<CustomerResponse> ListCustomers { get => listCustomers; set => listCustomers = value; }
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
            var addInvoice = _serviceProvider.GetRequiredService<IAddInvoiceView>();
            addInvoice.CustomerId = SelectedCustomer.Id;
            addInvoice.Show();
        }
    }
}