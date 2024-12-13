using WinFormsApplication.Interfaces;
using WinFormsApplication.Models.Input;
using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Views;

public partial class AddInvoiceView : Form, IAddInvoiceView
{
    private readonly IAddInvoiceViewModel _invoiceViewModel;

    public AddInvoiceView(IAddInvoiceViewModel invoiceViewModel)
    {
        InitializeComponent();
        this._invoiceViewModel = invoiceViewModel;
    }

    public Guid CustomerId { get; set; }
    private CustomerDetailResponse Customer { get; set; }
    private CreateInvoiceInput CreateInvoice { get; set; } = new();

    private async void AddInvoiceView_Load(object sender, EventArgs e)
    {
        try
        {
            Customer = await _invoiceViewModel.GetCustomerAsync(CustomerId.ToString());

            CreateInvoice.ProxyId = Customer.Company.Id;
            textBoxCompany.Text = Customer.Company.PublicName;
            AssignDefaultAddress();
            SetErrorTextBox();
            dataGridViewInvoiceLines.DataSource = CreateInvoice.InvoiceLines;
            textBoxVATNumber.DataBindings.Add("Text", CreateInvoice, nameof(CreateInvoice.VatNumber));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SetErrorTextBox()
    {
        comboBoxException.Items.Clear();
        foreach (ErrorResponse error in Customer.Errors)
        {
            comboBoxException.Items.Add(error.ToString());
        }
        // Could do message box, but find this less jarring. 
        if (Customer.Errors.Count > 0)
        {
            comboBoxException.DroppedDown = true;
            comboBoxException.Items.Insert(0, "Click to view errors");
        }
        else
        {
            comboBoxException.Items.Insert(0, "There are no errors");
        }
        comboBoxException.SelectedIndex = 0;
    }

    private void AssignDefaultAddress()
    {
        try
        {
            foreach (CustomerAddressResponse address in Customer.Addresses.Where(a => a.IsDefault))
            {
                textBoxStreetName.Text = address.StreetName;
                textBoxNumber.Text = address.Number.ToString();
            }
            if (string.IsNullOrWhiteSpace(textBoxStreetName.Text))
            {
                textBoxStreetName.Text = Customer.Addresses.First().StreetName;
                textBoxNumber.Text = Customer.Addresses.First().Number.ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void textBoxVATNumber_TextChanged(object sender, EventArgs e)
    {
        buttonSafeInvoice.Enabled = true;
    }

    private async void buttonSafeInvoice_Click(object sender, EventArgs e)
    {
        Customer = await _invoiceViewModel.CreateInvoiceAsync(CreateInvoice);
        SetErrorTextBox();
        if (Customer.Errors.Count < 1)
            this.Close();
    }

    private void dataGridViewInvoiceLines_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
        MessageBox.Show("Please enter a number");
    }
}