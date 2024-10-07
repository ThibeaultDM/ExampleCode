using QueasoFramework.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewInvoiceDataLayer.Objects
{
    [Table("InvoiceHeaders")]
    public class DO_InvoiceHeader : DataObjectBase
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// BTW number
        /// </summary>
        [MaxLength(13)]
        public string VATNumber { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int InvoiceNumber { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        /// <summary>
        /// Total amount that needs to be paid
        /// </summary>
        [Range(0, int.MaxValue)]
        public decimal? TotalAmount { get; set; }

        /// <summary>
        /// Total amount of taxes that need to be paid
        /// </summary>
        [Range(0, int.MaxValue)]
        public decimal? VATAmount { get; set; }

        /// <summary>
        /// Total amount of taxes that need to be paid
        /// </summary>
        [Range(0, int.MaxValue)]
        public decimal? Amount { get; set; }

        public virtual List<DO_InvoiceLine>? InvoiceLines { get; set; }
        public virtual Guid? CompanyProxyId { get; set; }
    }
}