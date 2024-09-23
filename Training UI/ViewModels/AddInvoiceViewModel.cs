using Training_UI.Interfaces;
using Training_UI.Models.Input;
using Training_UI.Models.Response;

namespace Training_UI.ViewModels
{
    public class AddInvoiceViewModel : IAddInvoiceViewModel
    {
        private IDataModel invoiceModel;
        private CustomerResponse _customer;

        public AddInvoiceViewModel(IDataModel customerModel)
        {
            this.invoiceModel = customerModel;
        }

        public CustomerResponse Customer { get => _customer; set => _customer = value; }

        public async Task GetCustomerAsync(string guid)
        {
            await invoiceModel.FetchAllCustomersAsync();

            Guid searchId = new Guid(guid);

            Customer = invoiceModel.Customers.SingleOrDefault(c => c.Id == searchId) ?? throw new Exception("No customer was found");

            Console.WriteLine("GetCustomerAsync");
        }

        public async Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput invoiceInput)
        {
            CustomerDetailResponse response = new();

            response = await invoiceModel.CreateInvoiceAsync(invoiceInput);

            return response;
        }
    }
}