using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using DataAccess.Enum;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class OrderRepository:BaseRepository<Order>,IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public Order? GetOrderByProcessing(int id)
    {
        return DbSet.FirstOrDefault(x => x.Id == id && x.OrderStatus == OrderStatus.Processing);
    }
}