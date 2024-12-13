namespace NewInvoiceDataLayer.Interfaces;

public interface IInvoiceNumberRepository
{
    Task<int> GetNextNumber();
}