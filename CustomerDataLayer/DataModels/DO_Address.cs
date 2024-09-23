using QueasoFramework.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerDataLayer.DataModels
{
    [Table("Addresses")]
    public class DO_Address : DataObjectBase
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public virtual List<DO_Company> Companies { get; set; }
        public virtual List<DO_Person> People { get; set; }
        public string City { get; set; }
        public int Postcode { get; set; }

        /// <summary>
        /// MaxLength(100)
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string StreetName { get; set; }

        [Required]
        public int HouseNumber { get; set; }
    }
}