using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entites;

public class OrderDetail : BaseEntity
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    //FK
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
}