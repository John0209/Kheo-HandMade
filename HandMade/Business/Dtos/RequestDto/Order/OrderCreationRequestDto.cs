using ClassLibrary1.Dtos.RequestDto.Product;

namespace ClassLibrary1.Dtos.RequestDto.Order;

public class OrderCreationRequestDto
{
    public int CustomerId { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public required List<OrderProductDto> Products { get; set; } = new List<OrderProductDto>();
}