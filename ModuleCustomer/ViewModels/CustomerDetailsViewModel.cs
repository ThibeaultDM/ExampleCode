using ModuleCustomer.Interfaces;
using ModuleCustomer.Models.Response;
using System.Text.Json;

namespace ModuleCustomer
{
    public class CustomerDetailsViewModel : BindableBase, INavigationAware
    {
        private CustomerResponse customer;
        private readonly IDataModel customerModel;
        private readonly IRegionManager regionManager;

        public DelegateCommand AddInvoiceCommand { get; private set; }
        public CustomerResponse Customer { get => customer; set => SetProperty(ref customer, value); }

        public CustomerDetailsViewModel(IDataModel customerModel, IRegionManager regionManager)
        {
            this.customerModel = customerModel;
            this.regionManager = regionManager;

            AddInvoiceCommand = new DelegateCommand(AddInvoice);
        }

        #region Navigation

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
                return false;
            else
                return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        #endregion Navigation

        private async void AddInvoice()
        {
            if (Customer != null)
            {
                CustomerDetailResponse customer = await customerModel.GetDetailResponseAsync(Customer.Id);

                NavigationParameters parameters = new()
                {
                    { "Customer", JsonSerializer.Serialize(customer) }
                };
                regionManager.RequestNavigate("CustomerRegion", "InvoiceView", parameters);
            }
        }
    }
}