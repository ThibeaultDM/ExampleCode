using Flurl.Util;
using ModuleInvoice.Interfaces;
using ModuleInvoice.Models.Input;
using ModuleInvoice.Models.Response;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;

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
        private ObservableCollection<CreateInvoiceLineInput> invoiceLines;

        public InvoiceViewModel(IDataModel InvoiceModel, IRegionManager regionManager)
        {
            Console.WriteLine("InvoiceViewModel constructor working");
            _invoiceModel = InvoiceModel;
            this.regionManager = regionManager;
            SaveInvoiceCommand = new DelegateCommand(SaveInvoice);

            InvoiceHeader = new();
            InvoiceLines = new();
        }

        #region Properties
        public int MyProperty { get; set; }
        public CustomerDetailResponse Customer { get => customer; set { customer = value; OnPropertyChanged(); } }
        public string CompanyName { get => companyName; set { companyName = value; OnPropertyChanged(); } }
        public string StreetName { get => streetName; set { streetName = value; OnPropertyChanged(); } }
        public string HouseNumber { get => houseNumber; set { houseNumber = value; OnPropertyChanged(); } }

        public ObservableCollection<CreateInvoiceLineInput> InvoiceLines
        {
            get => invoiceLines;
            set
            {
                invoiceLines = value;
                OnPropertyChanged();
            }
        }

        public List<ErrorResponse> Errors
        { get => errors; set { errors = value; OnPropertyChanged(); } }

        public CreateInvoiceInput InvoiceHeader
        { get => invoiceHeader; private set { SetProperty(ref invoiceHeader, value); } }

        public DelegateCommand SaveInvoiceCommand { get; private set; }
        public DelegateCommand AddInvoiceLineCommand { get; private set; }

        #endregion Properties

        private async void SaveInvoice()
        {
            Errors = new();

            try
            {
                if (InvoiceLines.Count > 0 && InvoiceHeader.VatNumber != null)
                {
                    foreach (CreateInvoiceLineInput invoiceLine in InvoiceLines)
                    {
                        InvoiceHeader.InvoiceLines.Add(invoiceLine);
                    }

                    CustomerDetailResponse response = await _invoiceModel.CreateInvoiceAsync(InvoiceHeader);

                    if (response.Errors.Count > 0)
                    {
                        Errors = response.Errors;
                    }
                    else
                    {
                        // TODO also doesn't work
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

        #region Helper methodes

        private void HandleException(Exception ex)
        {
            if (ex.InnerException != null)
                HandleException(ex.InnerException);
            else
            {
                Errors.Add(new() { ErrorMessage = ex.Message });
            }
        }

        #endregion Helper methodes

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
            CustomerDetailResponse customer = navigationContext.Parameters["CustomerId"] as CustomerDetailResponse;
            if (customer != null)
                return false;
            else
                return true;
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            CustomerDetailResponse customer;
            try
            {
                Customer = JsonSerializer.Deserialize<CustomerDetailResponse>(navigationContext.Parameters["Customer"].ToString()) ?? throw new Exception();
                InvoiceHeader.ProxyId = Customer.Company.Id;
                CompanyName = Customer.Company.PublicName;
                StreetName = Customer.Addresses.FirstOrDefault().StreetName;
                HouseNumber = Customer.Addresses.FirstOrDefault().Number.ToString();
                Errors = Customer.Errors;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong deserialize customer.");
            }
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Customer = null;
        }
        #endregion Navigation
    }
}