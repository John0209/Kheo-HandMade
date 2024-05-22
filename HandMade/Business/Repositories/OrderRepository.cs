using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class OrderRepository:BaseRepository<Order>,IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }
}