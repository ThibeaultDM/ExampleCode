using QueasoFramework.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewInvoiceDataLayer.Objects;

[Table("InvoiceLines")]
public class DO_InvoiceLine : DataObjectBase
{
    [Required]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    /// <summary>
    /// Total amount to be paid
    /// </summary>
    [Range(0, int.MaxValue)]
    public decimal LineAmount { get; set; }

    /// <summary>
    /// Total cost of goods
    /// </summary>
    [Range(0, int.MaxValue)]
    public decimal Amount { get; set; }

    /// <summary>
    /// Amount ordered
    /// </summary>
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue)]
    public decimal PricePerUnit { get; set; }

    /// <summary>
    /// Amount of taxes to be paid on goods
    /// </summary>
    [Range(0, int.MaxValue)]
    public decimal VATAmount { get; set; }

    /// <summary>
    /// Tax rate on the goods
    /// </summary>
    [Range(0, 128)]
    public decimal VATRate { get; set; }

    /// <summary>
    /// Description of product
    /// </summary>
    [MaxLength(100)]
    public string? Description { get; set; }

    // TODO What does virtual do exactly look it up, don't annoy Bjorn he explained it already
    public virtual Guid InvoiceHeaderId { get; set; }
}