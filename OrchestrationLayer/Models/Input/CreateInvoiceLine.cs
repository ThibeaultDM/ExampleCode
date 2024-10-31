using System.ComponentModel.DataAnnotations;

namespace Orchestration.Models.Input
{
    public class CreateInvoiceLine
    {
        public Guid InvoiceHeaderId { get; set; }

        [Range(0, 79228162514264337593543950335d)]
        public decimal VATRate { get; set; }

        [Range(0, 79228162514264337593543950335d)]
        public decimal PricePerUnit { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 0;

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}