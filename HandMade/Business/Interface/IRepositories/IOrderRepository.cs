using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Interface.IRepositories;

public interface IOrderRepository:IBaseRepository<Order>
{
    public Order? GetOrderByProcessing(int id);
    public Task<List<Order>> GetOrdersByStatus(OrderStatus? status, int userId);
    public Task<List<Order>> AdminGetOrder(OrderStatus? status);
}