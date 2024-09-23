namespace Training_UI.Models.Response
{
    public class InvoiceDetailResponse : BaseResponse
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Amount to be paid before taxes
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Amount to be paid with taxes
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Amount of taxes to be paid
        /// </summary>
        public decimal VatAmount { get; set; }

        public double InvoiceNumber { get; set; }
        public string VatNumber { get; set; }
        public bool IsPaid { get; set; }

        public List<InvoiceDetailResponse> InvoiceLines { get; set; }
    }
}