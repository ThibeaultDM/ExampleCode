using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerDataLayer.DataModels
{
    [Table("CredritInfo")]
    public class DO_CreditInfo
    {
        [Key]
        public Guid Id { get; set; }

        public decimal ToSpend { get; set; }
        public DO_Customer Customer { get; set; }
    }
}