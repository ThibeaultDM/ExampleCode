namespace WinFormsApplication.Interfaces;

public interface IAddInvoiceView : IDisposable
{
    Guid CustomerId { get; set; }

    void Show();
}