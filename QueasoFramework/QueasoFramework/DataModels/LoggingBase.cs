using System;
using System.ComponentModel.DataAnnotations;

namespace QueasoFramework.DataModels
{
    public abstract class LoggingBase : DataObjectBase
    {
        #region Default properties

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public int Type { get; set; }

        [Required]
        public string Namespace { get; set; }

        [Required]
        public string UseCase { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string InputParameters { get; set; }

        #endregion Default properties
    }
}