using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModuleInvoice.Models.Input
{
    public class CreateInvoiceLineInput : INotifyPropertyChanged
    {
        private decimal vATRate;
        private decimal pricePerUnit;
        private int quantity = 0;
        private string description = "Description";

        public Guid InvoiceHeaderId { get; set; }

        public decimal VATRate
        { get => vATRate; set { vATRate = value; OnPropertyChanged("VATRate"); } }

        public decimal PricePerUnit
        { get => pricePerUnit; set { pricePerUnit = value; OnPropertyChanged("PricePerUnit"); } }

        public int Quantity
        { get => quantity; set { quantity = value; OnPropertyChanged("Quantity"); } }

        public string Description
        { get => description; set { description = value; OnPropertyChanged("Description"); } }

        public override string ToString()
        {
            return $"Description: {Description}, quantity: {Quantity}, Price per unit: {PricePerUnit}, VAT: {VATRate}";
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}