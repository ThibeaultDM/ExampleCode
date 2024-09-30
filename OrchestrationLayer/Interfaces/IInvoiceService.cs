using Orchestration.Models.Input;
using Orchestration.Models.Response;

namespace Orchestration.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceDetailResponse> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber);

        Task<InvoiceResponse> UC_301_002_AddInvoiceLineToHeaderAsync(CreateInvoiceLine invoiceLineInput);

        Task<InvoiceDetailResponse> UC_301_003_GetInvoiceByNameAsync(Guid getInvoiceByIdInput);

        Task<InvoiceDetailResponse> UC_301_004_ArchiveJournalEntryForInvoiceAsync(ArchiveInvoiceJournalEntryInput archiveInvoiceJournal);

        Task<List<InvoiceResponse>> UC_301_005_GetAllInvoicesAsync();
    }
}