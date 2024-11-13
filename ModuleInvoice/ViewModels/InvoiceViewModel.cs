﻿using ModuleInvoice.Interfaces;
using ModuleInvoice.Models.Input;
using ModuleInvoice.Models.Response;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModuleInvoice
{
    public class InvoiceViewModel : BindableBase, INavigationAware, INotifyPropertyChanged
    {
        private IDataModel _invoiceModel;
        private readonly IRegionManager regionManager;
        private CustomerDetailResponse customer;
        private string companyName;
        private string streetName;
        private string houseNumber;
        private List<ErrorResponse> errors;
        private CreateInvoiceInput invoiceHeader;
        private CreateInvoiceLineInput invoiceToAdd;
        private List<CreateInvoiceLineInput> invoiceLines;

        public InvoiceViewModel(IDataModel InvoiceModel, IRegionManager regionManager)
        {
            Console.WriteLine("InvoiceViewModel constructor working");
            _invoiceModel = InvoiceModel;
            this.regionManager = regionManager;
            SaveInvoiceCommand = new DelegateCommand(SaveInvoice);
            AddInvoiceLineCommand = new DelegateCommand(AddInvoiceLine);

            InvoiceHeader = new();
            InvoiceLines = new();
            InvoiceToAdd = new();
        }

        #region Properties

        public CustomerDetailResponse Customer { get => customer; set => SetProperty(ref customer, value); }
        public string CompanyName { get => companyName; set => SetProperty(ref companyName, value); }
        public string StreetName { get => streetName; set => SetProperty(ref streetName, value); }
        public string HouseNumber { get => houseNumber; set => SetProperty(ref houseNumber, value); }

        public List<CreateInvoiceLineInput> InvoiceLines
        {
            get => invoiceLines;
            private set
            {
                invoiceLines = value;
                OnPropertyChanged();
            }
        }

        public List<ErrorResponse> Errors { get => errors; set { SetProperty(ref errors, value); } }
        public CreateInvoiceInput InvoiceHeader { get => invoiceHeader; private set { SetProperty(ref invoiceHeader, value); } }
        public CreateInvoiceLineInput InvoiceToAdd { get => invoiceToAdd; set => SetProperty(ref invoiceToAdd, value); }
        public DelegateCommand SaveInvoiceCommand { get; private set; }
        public DelegateCommand AddInvoiceLineCommand { get; private set; }

        #endregion Properties

        private void AddInvoiceLine()
        {
            Errors = new();
            try
            {
                InvoiceHeader.InvoiceLines.Add(InvoiceToAdd);

                InvoiceLines.Add(InvoiceToAdd);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void SaveInvoice()
        {
            Errors = new();

            try
            {
                if (InvoiceHeader.InvoiceLines.Count > 0 && InvoiceHeader.VatNumber != null)
                {
                    CustomerDetailResponse response = await _invoiceModel.CreateInvoiceAsync(InvoiceHeader);

                    if (response.Errors.Count > 0)
                    {
                        Errors = response.Errors;
                    }
                    else
                    {
                        // could add more func here
                        Errors.Add(new() { ErrorMessage = "Success" });
                    }
                }
                else
                {
                    Errors.Add(new() { ErrorMessage = "Please provide a VAT number and at minimum 1 invoice line" });
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            if (ex.InnerException != null)
                HandleException(ex.InnerException);
            else
            {
                Errors.Add(new() { ErrorMessage = ex.Message });
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged

        #region Navigation

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            string customer = navigationContext.Parameters["CustomerId"] as string;
            if (customer != null)
                return false;
            else
                return true;
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            string customerId = navigationContext.Parameters["CustomerId"] as string;

            if (customerId != null)
            {
                CustomerDetailResponse customer = await _invoiceModel.GetCustomerAsync(customerId);

                Customer = customer;
                InvoiceHeader.ProxyId = Customer.Company.Id;
                CompanyName = Customer.Company.PublicName;
                StreetName = Customer.Addresses.FirstOrDefault().StreetName;
                HouseNumber = Customer.Addresses.FirstOrDefault().Number.ToString();
                Errors = Customer.Errors;
            }
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Customer = null;
        }

        #endregion Navigation
    }
}