using Microsoft.Extensions.DependencyInjection;
using WinformsApplication.Interfaces;
using WinformsApplication.Models.Response;

namespace WinformsApplication.Views
{
    public partial class CustomerView : Form
    {
        private readonly ICustomerViewModel _customerViewModel;
        private readonly IServiceProvider _serviceProvider;

        public CustomerResponse SelectedCustomer { get; set; }

        public CustomerView(ICustomerViewModel customerViewModel, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this._customerViewModel = customerViewModel;
            this._serviceProvider = serviceProvider;
        }

        private async void CustomerView_Load(object sender, EventArgs e)
        {
            try
            {
                await _customerViewModel.GetCustomersAsync();

                comboBoxCustomers.Items.Insert(0, "Select a customer");
                comboBoxCustomers.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void comboBoxCustomers_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedCustomer = comboBoxCustomers.SelectedValue as CustomerResponse;

            if (SelectedCustomer == null) { }
            else
            {
                textBoxFirstName.Text = SelectedCustomer.FirstName;
                textBoxLastName.Text = SelectedCustomer.FamilyName;
                textBoxGender.Text = SelectedCustomer.Gender;
            }
        }

        private void comboBoxCustomers_DropDown(object sender, EventArgs e)
        {
            comboBoxCustomers.DataSource = _customerViewModel.ListCustomers;
            buttonAddInvoice.Enabled = true;
        }

        private void buttonAddInvoice_Click(object sender, EventArgs e)
        {
            AddInvoiceView addInvoice = _serviceProvider.GetRequiredService<AddInvoiceView>();
            addInvoice.CustomerId = SelectedCustomer.Id;
            addInvoice.Show();
        }
    }
}