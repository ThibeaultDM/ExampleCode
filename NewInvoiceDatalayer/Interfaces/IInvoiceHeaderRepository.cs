using NewInvoiceDataLayer.Objects;

namespace NewInvoiceDataLayer.Interfaces
{
    public interface IInvoiceHeaderRepository
    {
        /// <summary>
        /// Gets all invoiceHeaders in the database
        /// </summary>
        /// <returns></returns>
        Task<List<DO_InvoiceHeader>> GetInvoiceHeadersAsync();

        /// <summary>
        /// Creates an invoiceHeader in the database
        /// </summary>
        /// <param name="toCreate"></param>
        /// <returns></returns>
        Task<DO_InvoiceHeader> UC_301_001_CreateInvoiceHeaderAsync(DO_InvoiceHeader toCreate);

        /// <summary>
        /// Finds a specific invoiceHeader in the database
        /// </summary>
        /// <param name="toFind"></param>
        /// <returns></returns>
        Task<DO_InvoiceHeader> UC_301_003_FindInvoiceHeaderAsync(Guid toFind);

        /// <summary>
        /// Updates an invoiceHeader in the database
        /// </summary>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        Task<DO_InvoiceHeader> UpdateInvoiceHeaderAsync(DO_InvoiceHeader toUpdate);
    }
}