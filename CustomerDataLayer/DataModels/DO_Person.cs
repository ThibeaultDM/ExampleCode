using CustomerDataLayer.DataModels.Enums;
using QueasoFramework.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerDataLayer.DataModels
{
    public class DO_Person : DataObjectBase
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// MaxLength(100)
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string FamilyName { get; set; }

        /// <summary>
        /// MaxLength(50)
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public virtual List<DO_Address> Addresses { get; set; }
    }
}