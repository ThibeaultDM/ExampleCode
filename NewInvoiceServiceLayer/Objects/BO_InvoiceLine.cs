using NewInvoiceServiceLayer.Rules;
using QueasoFramework.BusinessModels;
using System.ComponentModel.DataAnnotations;

namespace NewInvoiceServiceLayer.Objects
{
    public class BO_InvoiceLine : BusinessObjectBase
    {
        private decimal _amount, _vatAmount, _lineAmount = 0;

        public Guid Id { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// Price of the products without taxes
        /// </summary>
        [Range(0, int.MaxValue)]
        public decimal Amount { get => _amount; private set => _amount = value; }

        /// <summary>
        /// Amount of tax to be paid on the products
        /// </summary>
        [Range(0, int.MaxValue)]
        public decimal VATAmount { get => _vatAmount; private set => _vatAmount = value; }

        [Range(0, int.MaxValue)]
        public decimal VATRate { get; set; }

        /// <summary>
        /// Total _amount to be paid
        /// </summary>
        [Range(0, int.MaxValue)]
        public decimal LineAmount { get => _lineAmount; private set => _lineAmount = value; }

        [Range(0, int.MaxValue)]
        public decimal PricePerUnit { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        public Guid InvoiceHeaderId { get; set; }

        public override bool AddBusinessRules()
        {
            BusinessRules.Clear();

            BusinessRules.Add(new InvoiceBusinessRules().IsRequired(nameof(PricePerUnit), PricePerUnit));
            BusinessRules.Add(new InvoiceBusinessRules().IsRequired(nameof(Quantity), Quantity));
            BusinessRules.Add(new InvoiceBusinessRules().IsRequired(nameof(VATRate), VATRate));
            BusinessRules.Add(new InvoiceBusinessRules().IsRequired(nameof(InvoiceHeaderId), InvoiceHeaderId));

            BusinessRules.Add(new InvoiceBusinessRules().RangeValue(nameof(VATAmount), VATAmount, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRules().RangeValue(nameof(VATRate), VATRate, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRules().RangeValue(nameof(PricePerUnit), PricePerUnit, 0, decimal.MaxValue));
            BusinessRules.Add(new InvoiceBusinessRules().RangeValue(nameof(Quantity), Quantity, 0, int.MaxValue));

            BusinessRules.Add(new InvoiceBusinessRules().CalculatedAmount_Line(nameof(Amount), this.Quantity, this.PricePerUnit, out this._amount));
            BusinessRules.Add(new InvoiceBusinessRules().CalculateVATAmount_Line(nameof(VATAmount), this.VATRate, this.Amount, out this._vatAmount));
            BusinessRules.Add(new InvoiceBusinessRules().GetSum(nameof(LineAmount), new List<decimal> { this.Amount, this.VATAmount }, out this._lineAmount));

            return base.AddBusinessRules();
        }

        public override string ToString()
        {
            return $"InvoiceLineBO with id: {Id}, InvoiceHeaderId: {InvoiceHeaderId}, Amount: {Amount}, VATAmount: {VATAmount}, VATRate: {VATRate}, LineAmount: {LineAmount}, PricePerUnit: {PricePerUnit}, Quantity: {Quantity}, Description: {Description},";
        }
    }
}