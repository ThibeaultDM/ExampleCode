﻿using ModuleCustomer.Interfaces;
using ModuleCustomer.Models.Response;

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

        private async void AddInvoice()
        {
            if (Customer != null)
            {
                CustomerDetailResponse detailResponse = await customerModel.GetCustomerAsync(Customer.Id.ToString());

                NavigationParameters parameters = new();
                parameters.Add("detailResponse", detailResponse);

                if (detailResponse != null)
                    regionManager.RequestNavigate("InvoiceRegion", "AddInvoiceView", parameters);

            }
        }
    }
}