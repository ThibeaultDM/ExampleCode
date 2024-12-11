using System.Security.AccessControl;
using WinformsApplication.Interfaces;
using WinformsApplication.Models.Response;
using System.Linq;
using WinformsApplication.Models.Input;
using System.Net;

namespace WinformsApplication.Views
{
    public partial class AddInvoiceView : Form
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
                AddNewRow();
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
            if (Customer.Errors.Count > 0)
                comboBoxException.DroppedDown = true;
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
            CreateInvoice.InvoiceLines.RemoveAt(CreateInvoice.InvoiceLines.Count - 1);
            Customer = await _invoiceViewModel.CreateInvoiceAsync(CreateInvoice);
            SetErrorTextBox();
        }

        private void dataGridViewInvoiceLines_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            AddNewRow();
        }

        //TODO find out how that dataGridView1.AllowUserToAddRows = true; works
        private void AddNewRow()
        {
            CreateInvoice.InvoiceLines.Add(new());
        }
    }
}