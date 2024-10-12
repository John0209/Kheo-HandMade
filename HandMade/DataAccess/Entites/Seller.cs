using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entites;

public class Seller : BaseEntity
{
    [MaxLength(500)] public string? Avarta { get; set; } = string.Empty;

    public decimal Wallet { get; set; } = 0;

    //Relation
    public int UserId { get; set; }
    public virtual User? User { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}