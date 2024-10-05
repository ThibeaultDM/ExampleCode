using NewInvoiceServiceLayer.Objects;

namespace NewInvoiceCommunicationLayer.Interfaces
{
    public interface IInvoiceUseCases
    {
        /// <summary>
        /// Creates an invoiceHeader
        /// </summary>
        /// <param name="vatNumber"></param>
        /// <returns></returns>
        Task<BO_InvoiceHeader> UC_301_001_CreateInvoiceHeaderAsync(string vatNumber);

        /// <summary>
        /// Add an invoiceLine to an invoiceHeader
        /// </summary>
        /// <param name="invoiceHeaderId"></param>
        /// <param name="boInvoiceLine"></param>
        /// <returns></returns>
        Task<BO_InvoiceHeader> UC_301_002_AddInvoiceLineToHeaderAsync(Guid invoiceHeaderId, BO_InvoiceLine boInvoiceLine);

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
        Task<BO_InvoiceHeader> UC_301_004_ArchiveJournalEntryForInvoiceAsync(Guid companyProxyId, Guid invoiceHeaderId);

        /// <summary>
        /// Gets all invoiceHeaders without their invoiceLines
        /// </summary>
        /// <returns></returns>
        Task<List<BO_InvoiceHeader>> UC_301_005_GetAllInvoicesHeadersAsync();
    }
}