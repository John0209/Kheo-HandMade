using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class ProductRepository:BaseRepository<Product>,IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}