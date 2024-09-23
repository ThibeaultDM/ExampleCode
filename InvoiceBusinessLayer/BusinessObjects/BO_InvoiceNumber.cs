using InvoiceDataLayer.Interfaces;

namespace InvoiceBusinessLayer.BusinessObjects
{
    public class BO_InvoiceNumber
    {
        // TODO have this reviewed
        // TODO research more on singleton injection and what is does in certain situations
        // what happens when a BO_InvoiceNumber is already being used, does it wait until it isn't?
        // Do I make LastNumber static? Does it also change the last number in the BO_InvoiceNumber that is being used
        // what is the purpose of this class the numbRepo takes care of this
        public BO_InvoiceNumber(IInvoiceNumberRepository numberRepository)
        {
            LastUsedNumber = numberRepository.GetNextNumber();
        }

        public int LastUsedNumber { get; private set; }
    }
}