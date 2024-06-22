using DataAccess.Enum;

namespace ClassLibrary1.Dtos.ResponseDto.Order;

public class OrderResponse
{
    public int Id { get; set; }
    public int OrderCode { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderDetailsDto> OrderDetails{ get; set; } = new List<OrderDetailsDto>();
    public int QuantityTotal { get; set; }
    public decimal PriceTotal { get; set; }
}