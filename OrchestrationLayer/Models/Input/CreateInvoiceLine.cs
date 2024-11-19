using System.ComponentModel.DataAnnotations;

namespace Orchestration.Models.Input
{
    public class CreateInvoiceLine
    {
        public Guid InvoiceHeaderId { get; set; }

        public decimal VATRate { get; set; }

        public decimal PricePerUnit { get; set; }

        public int Quantity { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}