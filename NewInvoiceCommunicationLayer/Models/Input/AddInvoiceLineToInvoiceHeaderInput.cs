using System.ComponentModel.DataAnnotations;

namespace NewInvoiceCommunicationLayer.Models.Input
{
    public class AddInvoiceLineToInvoiceHeaderInput
    {
        public Guid InvoiceHeaderId { get; set; }

        [Range(0, int.MaxValue)]
        public decimal VATRate { get; set; }

        [Range(0, int.MaxValue)]
        public decimal PricePerUnit { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; } = "AddAsync description";
    }
}