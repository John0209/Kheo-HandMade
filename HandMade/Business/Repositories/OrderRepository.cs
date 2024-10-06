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

    public async Task<List<Order>> GetOrdersByStatus(OrderStatus? status, int customerId)
    {
        var query = DbSet.AsNoTracking();
        query = query.Include(x => x.OrderDetails).ThenInclude(x => x.Product)
            .Where(x => x.CustomerId == customerId);
        if (status != null)
        {
            query = query.Where(x => x.OrderStatus == status);
        }

        return await query.ToListAsync();
    }

    public override Task<Order?> GetByIdAsync(int id, bool disableTracking = false)
    {
        return DbSet.Include(x => x.OrderDetails).ThenInclude(x => x.Product)
            .Include(x => x.Customer).ThenInclude(x => x!.User)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Order>> AdminGetOrder(OrderStatus? status)
    {
        return DbSet.Include(x => x.Customer).ThenInclude(x => x!.User)
            .Where(x => x.OrderStatus == status).ToListAsync();
    }
}