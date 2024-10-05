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
    [Table("InvoiceException")]
    public class DO_InvoiceException : DataObjectBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public string NameSpace { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string InputParameters { get; set; }
    }
}
