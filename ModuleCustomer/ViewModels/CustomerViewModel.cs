using ModuleCustomer.Interfaces;
using ModuleCustomer.Models.Response;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModuleCustomer
{
    public class CustomerViewModel : BindableBase, INotifyPropertyChanged
    {
        private IDataModel customerModel;
        private readonly IRegionManager regionManager;

        private List<CustomerResponse> listCustomers;

        public CustomerViewModel(IDataModel customerModel, IRegionManager regionManager)
        {
            Console.WriteLine("CustomerViewModel constructor working");

            this.customerModel = customerModel;
            this.regionManager = regionManager;
            CustomerSelectedCommand = new DelegateCommand<CustomerResponse>(CustomerSelected);
            GetCustomersAsync();
        }

        public List<CustomerResponse> ListCustomers
        {
            get => listCustomers;
            set
            {
                listCustomers = value;
                OnPropertyChanged();
            }
        }

        public async void GetCustomersAsync()
        {
            await customerModel.GetAllCustomersAsync();

            ListCustomers = customerModel.Customers;

            Console.WriteLine("FetchDataViewModel Customer ");
        }

        public DelegateCommand<CustomerResponse> CustomerSelectedCommand { get; private set; }

        private void CustomerSelected(CustomerResponse customer)
        {
            NavigationParameters parameters = new();
            parameters.Add("customer", customer);

            if (customer != null)
                regionManager.RequestNavigate("CustomerDetailsRegion", "CustomerDetailsView", parameters);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}