using InvoiceDataLayer.DataModels;

namespace InvoiceDataLayer.Interfaces
{
    public interface IInvoiceExceptionRepository : IDisposable
    {
        /// <summary>
        /// returns all teh encountered exceptions
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DO_InvoiceException>> GetExceptionsAsync();

        /// <summary>
        /// Store all encountered exceptions
        /// </summary>
        /// <param name="record"></param>
        Task CreateInvoiceExceptionsAsync(DO_InvoiceException record);

        /// <summary>
        /// Save all exceptions to the database
        /// </summary>
        Task SaveAsync();
    }
}