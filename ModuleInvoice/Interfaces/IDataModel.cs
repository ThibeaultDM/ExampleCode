using ModuleInvoice.Models.Input;
using ModuleInvoice.Models.Response;

namespace ModuleInvoice.Interfaces;

public interface IDataModel
{
    Task<CustomerDetailResponse> CreateInvoiceAsync(CreateInvoiceInput createInvoice);
}