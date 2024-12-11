using WinformsApplication.Interfaces;
using WinformsApplication.Models.Input;
using WinformsApplication.Models.Response;

namespace BlazorUI.ViewModels
{
    public class AddInvoiceViewModel : IAddInvoiceViewModel
    {
        private IDataModel invoiceModel;

        public AddInvoiceViewModel(IDataModel customerModel)
        {
            this.invoiceModel = customerModel;
        }

        public async Task<CustomerDetailResponse> GetCustomerAsync(string searchId)
        {
            CustomerDetailResponse customer = await invoiceModel.GetCustomerAsync(searchId);

            return customer;
        }

        public async Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput invoiceInput)
        {
            CustomerDetailResponse response = new();

            response = await invoiceModel.CreateInvoiceAsync(invoiceInput);

            return response;
        }
    }
}