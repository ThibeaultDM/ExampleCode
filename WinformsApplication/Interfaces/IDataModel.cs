using WinFormsApplication.Models.Input;
using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Interfaces;

public interface IDataModel
{
    List<CustomerResponse> Customers { get; }

    Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput createInvoice);

    Task GetAllCustomersAsync();

    Task<CustomerDetailResponse> GetCustomerAsync(string customerId);
}