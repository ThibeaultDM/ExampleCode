using WinFormsApplication.Interfaces;
using WinFormsApplication.Models.Response;

namespace BlazorUI.ViewModels
{
    public class CustomerViewModel : ICustomerViewModel
    {
        private IDataModel customerModel;
        private List<CustomerResponse> listCustomers;

        public CustomerViewModel(IDataModel customerModel)
        {
            Console.WriteLine("CustomerViewModel constructor working");
            this.customerModel = customerModel;
        }

        public List<CustomerResponse> ListCustomers { get => listCustomers; set => listCustomers = value; }

        public async Task GetCustomersAsync()
        {
            await customerModel.GetAllCustomersAsync();

            ListCustomers = customerModel.Customers;

            Console.WriteLine("FetchDataViewModel Customer ");
        }
    }
}