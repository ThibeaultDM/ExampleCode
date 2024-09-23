using Training_UI.Models.Input;
using Training_UI.Models.Response;

namespace Training_UI.Interfaces
{
    public interface IAddInvoiceViewModel
    {
        CustomerResponse Customer { get; set; }

        Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput invoiceInput);

        Task GetCustomerAsync(string guid);
    }
}