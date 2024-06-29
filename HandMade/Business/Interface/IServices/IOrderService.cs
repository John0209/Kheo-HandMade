using ClassLibrary1.Dtos.RequestDto.Order;
using ClassLibrary1.Dtos.ResponseDto.Order;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Interface.IServices;

public interface IOrderService
{
    Task<int> CreateOrderAsync(OrderCreationRequestDto dto, PaymentType type);
    Order? GetOrderByProcessing(int id);
    public Task UpdateOrderStatus(int id, OrderStatus status);
    public int GetIdMomoResponse(string id);
    public Task<int> CreateOrderTestAsync(OrderCreationRequestDto dto);
    public Task<List<OrderResponse>> GetOrders(OrderStatus? status,int customerId);
    public Task<OrderDetailsResponse> GetOrderDetails(int id);
    public  Task<OrderAdminResponse> AdminGetOrders(OrderStatus status);
}