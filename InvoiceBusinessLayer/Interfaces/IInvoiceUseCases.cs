using InvoiceBusinessLayer.BusinessObjects;
using QueasoFramework.BusinessModels.Rules;
using QueasoFramework.Exceptions;

namespace InvoiceBusinessLayer.Interfaces
{
    public interface IInvoiceUseCases
    {
        Task SaveInvoiceExceptionAsync(FrameworkException exception);

        Task<BO_InvoiceHeader> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber);

        Task<BO_InvoiceHeader> UC_301_002_AddInvoiceLineToHeaderAsync(Guid invoiceHeaderId, BO_InvoiceLine boInvoiceLine);

        Task<BO_InvoiceHeader> UC_301_003_GetInvoiceByNameAsync(Guid id);

        Task<BO_InvoiceHeader> UC_301_004_ArchiveJournalEntryForInvoiceAsync(Guid journalEntryId, Guid invoiceHeaderId);

        Task<List<BO_InvoiceHeader>> UC_301_005_GetAllInvoicesAsync();
    }
}