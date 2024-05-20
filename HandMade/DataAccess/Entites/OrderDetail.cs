using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entites;

[Index(nameof(ProductId), IsUnique = true)]
public class OrderDetail : BaseEntity
{
    public int Quantity { get; set; }

    public decimal Price { get; set; }

    //FK
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}