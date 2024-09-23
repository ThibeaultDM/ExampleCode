using QueasoFramework.BusinessModels.Rules;

namespace InvoiceBusinessLayer.BusinessObjects
{
    public class BO_JournalEntry
    {
        public BO_JournalEntry(Guid journalHeaderId, Guid invoiceHeaderId, List<BrokenRule> brokenRules)
        {
            Id = new Guid();
            this.InvoiceHeaderId = invoiceHeaderId;
            this.JournalHeaderId = journalHeaderId;
            this.BrokenRules = brokenRules;
        }

        public Guid Id { get; set; }
        public Guid InvoiceHeaderId { get; set; }
        public Guid JournalHeaderId { get; set; }

        public List<BrokenRule> BrokenRules { get; set; }
    }
}