using WinFormsApplication.Interfaces;
using WinFormsApplication.Models.Input;
using WinFormsApplication.Models.Response;

namespace WinFormsApplication.Controllers;

public class AddInvoiceViewModel : IAddInvoiceViewModel
{
    private IDataModel invoiceModel;

    public AddInvoiceViewModel(IDataModel customerModel)
    {
        invoiceModel = customerModel;
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