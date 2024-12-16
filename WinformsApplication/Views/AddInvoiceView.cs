using WinFormsApplication.Interfaces;

namespace WinFormsApplication.Views;
public partial class AddInvoiceView : Form, IAddInvoiceView
{
    private readonly IAddInvoiceController _invoiceController;
    private Guid _customerId;

    public AddInvoiceView(IAddInvoiceController invoiceController)
    {
        InitializeComponent();
        _invoiceController = invoiceController;
        _invoiceController.CustomerSearchCompleted += UpdateCustomer;
        _invoiceController.ThereIsAProblem += UpdateErrors;
    }

    public Guid CustomerId
    {
        get => _customerId;
        set
        {
            _customerId = value;
            PassOnCustomerId();
        }
    }

    private void PassOnCustomerId() => _invoiceController.IdCustomer = CustomerId.ToString();

    private void AddInvoiceView_Load(object sender, EventArgs e)
    {
        try
        {
            ComboBoxState("There are no problems");
            dataGridViewInvoiceLines.DataSource = _invoiceController.InvoiceToCreate.InvoiceLines;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UpdateCustomer()
    {
        try
        {
            textBoxCompany.Text = _invoiceController.Customer.Company.PublicName;
            textBoxNumber.Text = _invoiceController.Customer.Addresses.FirstOrDefault().Number.ToString();
            textBoxStreetName.Text = _invoiceController.Customer.Addresses.FirstOrDefault().StreetName;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UpdateErrors()
    {
        try
        {
            comboBoxException.Items.Clear();
            _invoiceController.Errors.ForEach(e => comboBoxException.Items.Add(e));
            ComboBoxState("There are exceptions");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private async void buttonSafeInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            _invoiceController.InvoiceToCreate.VatNumber = textBoxVATNumber.Text;

            await _invoiceController.CreateInvoiceAsync();
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

    private void ComboBoxState(string message)
    {
        comboBoxException.Items.Insert(0, message);
        comboBoxException.SelectedIndex = 0;
        comboBoxException.DroppedDown = true;
    }

    private void dataGridViewInvoiceLines_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
        MessageBox.Show("Please enter a number");
    }
}
