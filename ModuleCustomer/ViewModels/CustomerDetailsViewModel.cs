using ModuleCustomer.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleCustomer
{
    public class CustomerDetailsViewModel : BindableBase, INavigationAware
    {
        private CustomerResponse customer;

        public CustomerResponse Customer { get => customer; set => customer = value; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var customer = navigationContext.Parameters["customer"] as CustomerResponse;
            if (customer != null)
                Customer = customer;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var customer = navigationContext.Parameters["customer"] as CustomerResponse;
            if (customer != null)
                return Customer != null && Customer.FamilyName == customer.FamilyName;
            else
                return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
