using System.ComponentModel.DataAnnotations;

namespace Orchestration.Models.Input
{
    public class CreateInvoiceLine
    {
        public Guid InvoiceHeaderId { get; set; }

        [Range(int.MaxValue, 0)]
        public decimal VATRate { get; set; }

        [Range(int.MaxValue, 0)]
        public decimal PricePerUnit { get; set; }

        [Range(int.MaxValue, 0)]
        public int Quantity { get; set; } = 0;

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}