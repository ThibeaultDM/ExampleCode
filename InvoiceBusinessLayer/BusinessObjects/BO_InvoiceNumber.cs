using InvoiceDataLayer.Interfaces;

namespace InvoiceBusinessLayer.BusinessObjects
{
    public class BO_InvoiceNumber
    {
        public BO_InvoiceNumber(IInvoiceNumberRepository numberRepository)
        {
            LastUsedNumber = numberRepository.GetNextNumber();
        }

        public int LastUsedNumber { get; private set; }
    }
}