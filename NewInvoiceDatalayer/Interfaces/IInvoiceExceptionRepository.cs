using NewInvoiceDataLayer.Objects;

namespace NewInvoiceDataLayer.Interfaces;

public interface IInvoiceExceptionRepository
{
    /// <summary>
    /// Creates a InvoiceException in the database
    /// </summary>
    /// <param name="toCreate"></param>
    /// <returns></returns>
    Task<DO_InvoiceException> SaveInvoiceExceptionAsync(DO_InvoiceException toCreate);
}