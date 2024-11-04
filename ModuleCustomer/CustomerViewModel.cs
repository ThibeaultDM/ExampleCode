using ModuleCustomer.Interfaces;
using ModuleCustomer.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleCustomer
{
    public class CustomerViewModel: BindableBase
    {
        private IDataModel customerModel;
        private List<CustomerResponse> listCustomers;

        public CustomerViewModel(IDataModel customerModel)
        {
            Console.WriteLine("CustomerViewModel constructor working");
            this.customerModel = customerModel;
            GetCustomersAsync();
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
