using QueasoFramework.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerDataLayer.DataModels
{
    [Table("InvoiceHeaderProxy")]
    public class DO_InvoiceHeaderProxy : DataObjectBase
    {
        #region Properties

        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DO_Company Company { get; set; }

        #endregion Properties
    }
}