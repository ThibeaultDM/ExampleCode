using InvoiceBusinessLayer.Rules;
using QueasoFramework.BusinessModels;
using QueasoFramework.BusinessModels.Rules;

namespace InvoiceBusinessLayer.BusinessObjects
{
    public class BO_InvoiceHeader : BusinessObjectBase
    {
        private decimal _amount, _vatAmount, _totalAmount = 0;

        public BO_InvoiceHeader()
        {
            Id = new Guid();
            VatNumber = string.Empty;
            InvoiceLines = new List<BO_InvoiceLine>();
            BusinessRules = new List<BusinessRule>();
        }

        public BO_InvoiceHeader(string vatNumber)
        {
            Id = new Guid();
            VatNumber = vatNumber;
            InvoiceLines = new List<BO_InvoiceLine>();
            BusinessRules = new List<BusinessRule>();
        }

        public Guid Id { get; set; }
        public Guid? ProxyIdCompany { get; set; }

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
        public List<BO_InvoiceLine> InvoiceLines { get; set; }

        public void AddInvoiceLineToHeader(BO_InvoiceLine invoiceLine)
        {
            if (InvoiceLines == null)
            {
                InvoiceLines = new List<BO_InvoiceLine>();
            }

            InvoiceLines.Add(invoiceLine);
            BrokenRules.Clear();
            AddBusinessRules();
        }

        public override bool AddBusinessRules()
        {
            BusinessRules.Add(new InvoiceBusinessRule().IsRequired(nameof(VatNumber), VatNumber));

            BusinessRules.Add(new InvoiceBusinessRule().MaxLength(nameof(VatNumber), VatNumber, 50));

            BusinessRules.Add(new InvoiceBusinessRule().CheckValidityVatNumber(nameof(this.VatNumber), this.VatNumber));

            BusinessRules.Add(new InvoiceBusinessRule().RangeValue(nameof(Amount), Amount, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRule().RangeValue(nameof(TotalAmount), TotalAmount, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRule().RangeValue(nameof(VatAmount), VatAmount, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRule().RangeValue(nameof(InvoiceNumber), (decimal)InvoiceNumber, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRule().IsRequired(nameof(InvoiceLines), InvoiceLines));

            BusinessRules.Add(new InvoiceBusinessRule().CalculateTotal_Invoice(nameof(this.Amount), this.InvoiceLines, out _amount));

            BusinessRules.Add(new InvoiceBusinessRule().CalculateVatAmount_Invoice(nameof(this.VatAmount), this.InvoiceLines, out _vatAmount));

            BusinessRules.Add(new InvoiceBusinessRule().GetSum(nameof(this.TotalAmount), new List<decimal> { this.Amount, this.VatAmount }, out this._totalAmount));

            return base.AddBusinessRules();
        }
    }
}