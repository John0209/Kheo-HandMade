using DataAccess.Enum;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entites;

public class Order : BaseEntity
{
    public int OrderCode { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }

    //Fk
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}