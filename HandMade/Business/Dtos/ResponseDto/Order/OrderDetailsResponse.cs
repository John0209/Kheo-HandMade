using DataAccess.Enum;

namespace ClassLibrary1.Dtos.ResponseDto.Order;

public class OrderDetailsResponse
{
    public int Id { get; set; }
    public int OrderCode { get; set; }
    public int QuantityTotal { get; set; }
    public decimal PriceTotal { get; set; }
    public OrderStatus Status { get; set; }
    public string? OrderDate { get; set; }
    public PaymentType PaymentType { get; set; }
    public List<OrderDetailsDto> OrderDetails{ get; set; } = new List<OrderDetailsDto>();
}