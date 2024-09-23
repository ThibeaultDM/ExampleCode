using QueasoFramework.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceDataLayer.DataModels
{
    [Table("InvoiceHeader")]
    public class DO_InvoiceHeader : DataObjectBase
    {
        #region Properties

        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        [Required]
        public int InvoiceNumber { get; set; }

        /// <summary>
        /// Total price without tax
        /// </summary>
        [Range(0, 79228162514264337593543950335d)]// Max value of a decimal
        public decimal Amount { get; set; }

        /// <summary>
        /// Total taxes
        /// </summary>
        [Range(0, 79228162514264337593543950335d)]
        public decimal VatAmount { get; set; }

        /// <summary>
        /// Total price facture
        /// </summary>
        [Range(0, 79228162514264337593543950335d)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Btw number
        /// </summary>
        [MaxLength(50)]
        public string VatNumber { get; set; }

        #endregion Properties

        #region Relations

        public virtual List<DO_InvoiceLine> InvoiceLines { get; set; }

        #endregion Relations
    }
}