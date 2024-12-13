using QueasoFramework.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewInvoiceDataLayer.Objects;

[Table("InvoiceNumber")]
public class DO_InvoiceNumber : DataObjectBase
{
    // https://csharpindepth.com/articles/singleton
    private static volatile DO_InvoiceNumber instance;

    private static readonly object synceRoot = new();

    [Required]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public int LastUsedNumber { get; set; }

    public static DO_InvoiceNumber Instance
    {
        get
        {
            if (instance == null)
            {
                lock (synceRoot)
                {
                    instance = new DO_InvoiceNumber();
                }
            }
            return instance;
        }
    }
}