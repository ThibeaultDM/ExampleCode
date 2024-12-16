using WinFormsApplication.Controllers;
using WinFormsApplication.Interfaces;
using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Views;

public partial class CustomerView : Form, ICustomerView
{
    private readonly ICustomerController _customerViewModel;

    public CustomerView(ICustomerController customerViewModel)
    {
        InitializeComponent();
        _customerViewModel = customerViewModel;
        _customerViewModel.DataLoad += Controller_DataLoaded;
    }

    private async void CustomerView_Load(object sender, EventArgs e)
    {
        comboBoxCustomers.Items.Insert(0, "Loading customers");
        comboBoxCustomers.SelectedIndex = 0;

        await _customerViewModel.GetCustomersAsync();
    }

    private void comboBoxCustomers_SelectedValueChanged(object sender, EventArgs e)
    {
        if (comboBoxCustomers.SelectedValue != null)
            _customerViewModel.SelectedCustomer = comboBoxCustomers.SelectedValue as CustomerResponse;

        if (_customerViewModel.SelectedCustomer != null)
        {
            textBoxFirstName.Text = _customerViewModel.SelectedCustomer.FirstName;
            textBoxLastName.Text = _customerViewModel.SelectedCustomer.FamilyName;
            textBoxGender.Text = _customerViewModel.SelectedCustomer.Gender;
        }
    }

    private void buttonAddInvoice_Click(object sender, EventArgs e)
    {
        _customerViewModel.AddInvoiceAction.Invoke();
    }

    private void Controller_DataLoaded()
    {
        comboBoxCustomers.Items.Clear();
        comboBoxCustomers.DataSource = _customerViewModel.Customers;
        comboBoxCustomers.DroppedDown = true;
    }

    private void comboBoxCustomers_DropDown(object sender, EventArgs e)
    {
        buttonAddInvoice.Enabled = true;
    }
}