using NewInvoiceBusinessLayer.Objects;
using NewInvoiceServiceLayer.Objects;

namespace NewInvoiceServiceLayer.Interfaces;

public interface IInvoiceUseCases
{
    /// <summary>
    /// Creates an invoiceHeader
    /// </summary>
    /// <param name="vatNumber"></param>
    /// <returns></returns>
    Task<BO_InvoiceHeader> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber, string proxyCompanyId);

    /// <summary>
    /// AddAsync an invoiceLine to an invoiceHeader
    /// </summary>
    /// <param name="invoiceHeaderId"></param>
    /// <param name="boInvoiceLine"></param>
    /// <returns></returns>
    Task<BO_InvoiceHeader> UC_301_002_AddInvoiceLineToHeaderAsync(BO_InvoiceLine input);

    /// <summary>
    /// Finds an invoiceHeader and it's corresponding invoiceLines by the invoiceHeaders id
    /// </summary>
    /// <param name="toFind"></param>
    /// <returns></returns>
    Task<BO_InvoiceHeader> UC_301_003_FindInvoiceHeaderAsync(Guid toFind);

    /// <summary>
    /// Links an existing invoiceHeader to an existing company
    /// </summary>
    /// <param name="companyProxyId"></param>
    /// <param name="invoiceHeaderId"></param>
    /// <returns></returns>
    Task<BO_JournalEntry> UC_301_004_ArchiveJournalEntryForInvoiceAsync(Guid journalEntryId, Guid InvoiceHeaderId);

    /// <summary>
    /// Gets all invoiceHeaders without their invoiceLines
    /// </summary>
    /// <returns></returns>
    Task<List<BO_InvoiceHeader>> UC_301_005_GetAllInvoicesHeadersAsync();

    /// <summary>
    /// Adds an InvoiceException to the database
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    //Task SaveInvoiceExceptionAsync(FrameworkException exception);
}