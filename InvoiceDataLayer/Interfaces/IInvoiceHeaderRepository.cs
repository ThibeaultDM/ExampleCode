using InvoiceDataLayer.DataModels;

namespace InvoiceDataLayer.Interfaces
{
    public interface IInvoiceHeaderRepository
    {
        Task<List<DO_InvoiceHeader>> GetInvoiceHeadersAsync();

        /// <summary>
        /// Return requested invoice header
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DO_InvoiceHeader> GetInvoiceHeaderAsync(Guid Id);

        /// <summary>
        /// Create a new invoice header
        /// </summary>
        /// <param name="record"></param>
        Task<DO_InvoiceHeader> CreateInvoiceHeaderAsync(DO_InvoiceHeader record);

        /// <summary>
        /// Update an invoice header
        /// </summary>
        /// <param name="record"></param>
        Task<DO_InvoiceHeader> UpdateInvoiceHeaderAsync(DO_InvoiceHeader record);

        /// <summary>
        /// Update an invoice header for adding a invoiceLine
        /// </summary>
        /// <param name="record"></param>
        /// <param name="recordInvoiceLine"></param>
        /// <returns></returns>
        Task<DO_InvoiceHeader> UpdateInvoiceHeaderAsync(DO_InvoiceHeader record, DO_InvoiceLine recordInvoiceLine);

        /// <summary>
        /// Save to the database
        /// </summary>
        Task SaveAsync();
    }
}