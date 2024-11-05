using ModuleCustomer.Interfaces;
using ModuleCustomer.Models.Response;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModuleCustomer
{
    public class CustomerViewModel : BindableBase, INotifyPropertyChanged
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
            set 
            {
                SetProperty(ref _title, value);
                OnPropertyChanged();
            }
        }

        public List<CustomerResponse> ListCustomers { get => listCustomers; 
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
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion //INotifyPropertyChanged

    }
}