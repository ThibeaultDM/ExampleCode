using NewInvoiceCommunicationLayer.Models.Response;
using System.ComponentModel.DataAnnotations;

namespace NewInvoiceCommunicationLayer.Models.Input
{
    public class AddInvoiceLineToInvoiceHeaderInput : BaseResponse
    {
        public Guid InvoiceHeaderId { get; set; }

        [Range(int.MaxValue, 0)]
        public decimal VATRate { get; set; }

        [Range(int.MaxValue, 0)]
        public decimal PricePerUnit { get; set; }

        [Range(int.MaxValue, 0)]
        public int Quantity { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}