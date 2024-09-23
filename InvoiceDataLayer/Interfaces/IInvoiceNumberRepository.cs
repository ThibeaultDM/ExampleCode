namespace InvoiceDataLayer.Interfaces
{
    public interface IInvoiceNumberRepository : IDisposable
    {
        /// <summary>
        /// Return the invoice number
        /// </summary>
        /// <returns></returns>
        int GetNextNumber();

        /// <summary>
        /// Save to the database
        /// </summary>
        void Save();
    }
}