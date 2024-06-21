using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using DataAccess.Enum;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public Order? GetOrderByProcessing(int id)
    {
        return DbSet.FirstOrDefault(x => x.Id == id && x.OrderStatus == OrderStatus.Processing);
    }

    public async Task<List<Order>> GetOrdersByStatus(OrderStatus status, int customerId)
    {
        return await DbSet.Include(x => x.OrderDetails).ThenInclude(x=>x.Product)
            .Where(x => x.OrderStatus == status && x.CustomerId == customerId).ToListAsync();
    }

    public override Task<Order?> GetByIdAsync(int id, bool disableTracking = false)
    {
        return DbSet.Include(x => x.OrderDetails).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}