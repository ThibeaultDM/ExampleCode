using InvoiceBusinessLayer.Rules;
using QueasoFramework.BusinessModels;
using System.ComponentModel.DataAnnotations;

namespace InvoiceBusinessLayer.BusinessObjects
{
    public class BO_InvoiceLine : BusinessObjectBase
    {
        private decimal amount, vatAmount, lineAmount = 0;

        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Price of all the items without taxes
        /// </summary>
        [Range(0, 79228162514264337593543950335d)]// Max value of a decimal
        public decimal Amount { get => amount; set => amount = value; }

        /// <summary>
        /// Amount of taxes on total amount of products
        /// </summary>
        [Range(0, 79228162514264337593543950335d)]
        public decimal VATAmount { get => vatAmount; set => vatAmount = value; }

        [Range(0, 79228162514264337593543950335d)]
        public decimal VATRate { get; set; }

        /// <summary>
        /// Total price
        /// </summary>
        [Range(0, 79228162514264337593543950335d)]
        public decimal LineAmount { get => lineAmount; set => lineAmount = value; }

        [Range(0, 79228162514264337593543950335d)]
        public decimal PricePerUnit { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public override bool AddBusinessRules()
        {
            BusinessRules.Add(new InvoiceBusinessRule().RangeValue(nameof(Amount), Amount, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRule().RangeValue(nameof(VATAmount), VATAmount, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRule().RangeValue(nameof(VATRate), VATRate, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRule().RangeValue(nameof(LineAmount), LineAmount, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRule().RangeValue(nameof(PricePerUnit), PricePerUnit, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRule().RangeValue(nameof(Quantity), Quantity, 0, int.MaxValue));

            BusinessRules.Add(new InvoiceBusinessRule().IsRequired(nameof(PricePerUnit), PricePerUnit));
            BusinessRules.Add(new InvoiceBusinessRule().IsRequired(nameof(Quantity), Quantity));
            BusinessRules.Add(new InvoiceBusinessRule().IsRequired(nameof(VATRate), VATRate));

            BusinessRules.Add(new InvoiceBusinessRule().CalculatedAmount_Line(nameof(Amount), this.Quantity, this.PricePerUnit, out this.amount));
            BusinessRules.Add(new InvoiceBusinessRule().CalculateVATAmount_Line(nameof(VATAmount), this.VATRate, this.Amount, out this.vatAmount));
            BusinessRules.Add(new InvoiceBusinessRule().GetSum(nameof(LineAmount), new List<decimal> { this.Amount, this.VATAmount }, out this.lineAmount));

            return base.AddBusinessRules();
        }
    }
}