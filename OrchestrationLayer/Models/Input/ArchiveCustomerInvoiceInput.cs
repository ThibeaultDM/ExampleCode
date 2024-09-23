namespace Orchestration.Models.Input
{
    public class ArchiveCustomerInvoiceInput
    {
        public Guid CustomerId { get; set; }
        public Guid InvoiceId { get; set; }
    }
}