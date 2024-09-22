using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class OrderDetailRepository:BaseRepository<OrderDetail>,IOrderDetailRepository
{
    public OrderDetailRepository(AppDbContext context) : base(context)
    {
    }
}