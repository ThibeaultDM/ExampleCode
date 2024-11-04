using ModuleCustomer.Interfaces;

namespace ModuleInvoice
{
    public class InvoiceViewModel : BindableBase
    {
        private IDataModel _invoiceModel;
        private string _title = "Add Invoice";

        public InvoiceViewModel(IDataModel customerModel)
        {
            Console.WriteLine("InvoiceViewModel constructor working");
            this._invoiceModel = customerModel;
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}