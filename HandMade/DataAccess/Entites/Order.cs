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
    public PaymentType PaymentType { get; set; }

    //Fk
    public int CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; }= new List<OrderDetail>();
}