using ClassLibrary1.Dtos.RequestDto.Order;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Interface.IServices;

public interface IOrderService
{
    Task<int> CreateOrderAsync(OrderCreationRequestDto dto);
    Order? GetOrderByProcessing(int id);
    public Task UpdateOrderStatus(int id, OrderStatus status);
    public int GetIdMomoResponse(string id);
}