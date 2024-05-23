using System.ComponentModel.DataAnnotations;

namespace DataAccess.Enum;

public enum ProductStatus
{
    [Display(Name = "Stocking")]
    Stocking=1,
    [Display(Name = "OutOfStock")]
    OutOfStock=2,
    [Display(Name = "Hide")]
    Hide=3
}