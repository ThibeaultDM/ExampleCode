using System;
using System.ComponentModel.DataAnnotations;

namespace QueasoFramework.DataModels;

public abstract class DataObjectBase
{
    #region Creation

    [Required]
    public string CreatedBy { get; set; } = Environment.UserName;

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    #endregion Creation

    #region Updated

    public string UpdatedBy { get; set; } = null;
    public DateTime? UpdatedOn { get; set; } = null;

    #endregion Updated

    #region Deleted

    public bool IsDeleted { get; set; } = false;
    public string DeletedBy { get; set; } = null;
    public DateTime? DeletedOn { get; set; } = null;

    #endregion Deleted
}