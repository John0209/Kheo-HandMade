using DataAccess.Enum;

namespace ClassLibrary1.Dtos.ResponseDto.Order;

public class OrderAdminResponse
{
   public int TotalOrder { get; set; }
   public List<OrderAdminDto> Orders { get; set; } = new List<OrderAdminDto>();
}