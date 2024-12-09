using Orchestration.Models.Input;
using Orchestration.Models.Response;

namespace Orchestration.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceDetailResponse> UC_301_001_CreateInvoiceHeaderAsync(CreateInvoiceHeaderInput input);

        Task<InvoiceResponse> UC_301_002_AddInvoiceLineToHeaderAsync(CreateInvoiceLine invoiceLineInput);

        Task<InvoiceDetailResponse> UC_301_003_GetInvoiceByNameAsync(Guid getInvoiceByIdInput);

        Task<ArchiveInvoiceJournalEntryResponse> UC_301_004_ArchiveJournalEntryForInvoiceAsync(ArchiveInvoiceJournalEntry archiveInvoiceJournal);

        Task<List<InvoiceResponse>> UC_301_005_GetAllInvoicesAsync();
    }
}