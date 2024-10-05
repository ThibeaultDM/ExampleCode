using NewInvoiceServiceLayer.Rules;
using QueasoFramework.BusinessModels;

namespace NewInvoiceServiceLayer.Objects
{
    public class BO_InvoiceHeader : BusinessObjectBase
    {
        private decimal _amount, _vatAmount, _totalAmount = 0;

        public BO_InvoiceHeader(string vatNumber)
        {
            VatNumber = vatNumber;
            IsPaid = false;
        }

        public Guid Id { get; set; }

        /// <summary>
        /// Amount to be paid before taxes
        /// </summary>
        public decimal Amount { get => _amount; set => _amount = value; }

        /// <summary>
        /// Amount to be paid with taxes
        /// </summary>
        public decimal TotalAmount { get => _totalAmount; set => _totalAmount = value; }

        /// <summary>
        /// Amount of taxes to be paid
        /// </summary>
        public decimal VatAmount { get => _vatAmount; set => _vatAmount = value; }

        public int InvoiceNumber { get; set; }
        public string VatNumber { get; set; }
        public bool IsPaid { get; set; }
        public List<BO_InvoiceLine> InvoiceLines { get; private set; }

        public void AddInvoiceLineToHeader(BO_InvoiceLine invoiceLine)
        {
            if (InvoiceLines == null)
            {
                InvoiceLines = new();
            }

            InvoiceLines.Add(invoiceLine);
            BrokenRules.Clear();
            AddBusinessRules();
        }

        public override bool AddBusinessRules()
        {
            Console.WriteLine($"{Id} is getting checked");

            BusinessRules.Add(new InvoiceBusinessRules().IsRequired(nameof(VatNumber), VatNumber));
            BusinessRules.Add(new InvoiceBusinessRules().IsRequired(nameof(InvoiceLines), InvoiceLines));
            BusinessRules.Add(new InvoiceBusinessRules().IsRequired(nameof(InvoiceNumber), InvoiceNumber));

            BusinessRules.Add(new InvoiceBusinessRules().MaxLength(nameof(VatNumber), VatNumber, 13));
            BusinessRules.Add(new InvoiceBusinessRules().RangeValue(nameof(Amount), Amount, 0, int.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRules().RangeValue(nameof(TotalAmount), TotalAmount, 0, int.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRules().RangeValue(nameof(VatAmount), VatAmount, 0, int.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRules().RangeValue(nameof(InvoiceNumber), InvoiceNumber, 0, int.MaxValue));

            BusinessRules.Add(new InvoiceBusinessRules().CheckValidityVatNumber(nameof(this.VatNumber), this.VatNumber));
            BusinessRules.Add(new InvoiceBusinessRules().CalculateTotal_Invoice(nameof(this.Amount), this.InvoiceLines, out _amount));
            BusinessRules.Add(new InvoiceBusinessRules().CalculateVatAmount_Invoice(nameof(this.VatAmount), this.InvoiceLines, out _vatAmount));
            BusinessRules.Add(new InvoiceBusinessRules().GetSum(nameof(this.TotalAmount), new List<decimal> { this.Amount, this.VatAmount }, out this._totalAmount));

            return base.AddBusinessRules();
        }
    }
}