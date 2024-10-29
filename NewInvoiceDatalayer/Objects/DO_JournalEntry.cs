using QueasoFramework.DataModels;

namespace NewInvoiceDataLayer.Objects
{
    public class DO_JournalEntry : DataObjectBase
    {
        public Guid Id { get; set; }
        public Guid JournalHeaderId { get; set; }
        public Guid InvoiceHeaderId { get; set; }
    }
}