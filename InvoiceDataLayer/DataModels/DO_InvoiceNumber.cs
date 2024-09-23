using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceDataLayer.DataModels
{
    [Table("InvoiceNumber")]
    public sealed class DO_InvoiceNumber
    {
        private static volatile DO_InvoiceNumber instance;

        private static readonly object synceRoot = new object();

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public int LastUsedNumber { get; set; }

        public static DO_InvoiceNumber Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (synceRoot)
                    {
                        instance = new DO_InvoiceNumber();
                    }
                }
                return instance;
            }
        }
    }
}