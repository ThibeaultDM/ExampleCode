namespace Training_UI.Models.Response

{
    public class InvoiceLineDetailResponse : BaseResponse
    {
        private decimal _Amount;
        private decimal _VatAmount;
        private decimal _LineAmount;

        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public decimal VATAmount
        {
            get { return _VatAmount; }
            set { _VatAmount = value; }
        }

        public decimal VATRate { get; set; }

        public decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }

        public decimal PricePerUnit { get; set; }

        public int Quantity { get; set; } = 0;

        public string Description { get; set; }
    }
}