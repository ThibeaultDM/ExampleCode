using QueasoFramework.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerDataLayer.DataModels;

[Table("Companies")]
public class DO_Company : DataObjectBase
{
    [Required]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    /// <summary>
    /// MaxLength(100)
    /// </summary>
    [MaxLength(100)]
    [Required]
    public string PublicName { get; set; }

    public bool IsActive { get; set; }
    public virtual List<DO_Address> Addresses { get; set; }
    public DO_Customer Customer { get; set; }
}