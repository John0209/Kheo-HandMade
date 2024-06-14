using DataAccess.Entites;

namespace ClassLibrary1.Interface.IRepositories;

public interface IOrderRepository:IBaseRepository<Order>
{
    public Order? GetOrderByProcessing(int id);
}