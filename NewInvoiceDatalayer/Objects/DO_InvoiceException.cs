using QueasoFramework.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewInvoiceDataLayer.Objects
{
    [Table("InvoiceException")]
    public class DO_InvoiceException : DataObjectBase
    {
        [Required]
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