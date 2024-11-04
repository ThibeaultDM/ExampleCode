using ModuleCustomer.Interfaces;
using ModuleCustomer.Models.Response;

namespace ModuleCustomer
{
    public class CustomerViewModel : BindableBase
    {
        private IDataModel customerModel;
        private List<CustomerResponse> listCustomers;
        private string _title = "Customers";

        public CustomerViewModel(IDataModel customerModel)
        {
            Console.WriteLine("CustomerViewModel constructor working");
            this.customerModel = customerModel;
            GetCustomersAsync();
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public List<CustomerResponse> ListCustomers { get => listCustomers; set => listCustomers = value; }

        public async void GetCustomersAsync()
        {
            await customerModel.GetAllCustomersAsync();

            ListCustomers = customerModel.Customers;

            Console.WriteLine("FetchDataViewModel Customer ");

        }
    }
}