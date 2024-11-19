using NewInvoiceCommunicationLayer.Models.Response;
using System.ComponentModel.DataAnnotations;

namespace NewInvoiceCommunicationLayer.Models.Input
{
    public class AddInvoiceLineToInvoiceHeaderInput : BaseResponse
    {
        public Guid InvoiceHeaderId { get; set; }

        public decimal VATRate { get; set; }

        public decimal PricePerUnit { get; set; }

        public int Quantity { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}