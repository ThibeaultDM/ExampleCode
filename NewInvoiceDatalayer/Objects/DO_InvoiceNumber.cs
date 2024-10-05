using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueasoFramework.DataModels;

namespace NewInvoiceDataLayer.Objects
{
    [Table("InvoiceNumber")]
    public class DO_InvoiceNumber : DataObjectBase
    {
        // https://csharpindepth.com/articles/singleton
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
