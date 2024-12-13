using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerDataLayer.DataModels;

[Table("Customers")]
public class DO_Customer : DO_Person
{
    [Column(TypeName = "bit")]
    public bool IsActive { get; set; }

    [Required]
    public DO_CreditInfo CreditInfo { get; set; }

    public DO_Company Company { get; set; }
}