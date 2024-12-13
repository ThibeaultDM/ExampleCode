using WinFormsApplication.Models.Input;
using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Interfaces;

public interface IAddInvoiceViewModel
{
    Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput invoiceInput);

    Task<CustomerDetailResponse> GetCustomerAsync(string searchId);
}