using DataAccess.Enum;

namespace ClassLibrary1.Dtos.ResponseDto.Order;

public class OrderAdminDto
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public int OrderCode { get; set; }
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }
}