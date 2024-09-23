using QueasoFramework.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerDataLayer.DataModels
{
    [Table("CustomerException")]
    public class DO_CustomerException : DataObjectBase
    {
        [Key]
        public Guid Id { get; set; }

        public int ExceptionType { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public string Namespace { get; set; }

        [Required]
        public string UseCases { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string InputParameters { get; set; }
    }
}