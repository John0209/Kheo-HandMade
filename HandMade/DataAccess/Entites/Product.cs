using System.ComponentModel.DataAnnotations;
using DataAccess.Enum;

namespace DataAccess.Entites;

public class Product : BaseEntity
{
    [MaxLength(100)] public string? ProductName { get; set; } = string.Empty;
    [MaxLength(1000)] public string? Description { get; set; } = string.Empty;
    [MaxLength(500)] public string? Picture { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public ProductStatus Status { get; set; }

    //FK
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }

    public int SellerId { get; set; }
    public virtual Seller? Seller { get; set; }
}