using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using DataAccess.Enum;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<Product>> GetProductsAsync(ProductStatus status)
    {
        IQueryable<Product> query = DbSet.AsNoTracking();
        switch (status)
        {
            case ProductStatus.Hide:
                return query.Include(x => x.Category).Where(x => x.Status == ProductStatus.Hide).ToListAsync();
            case ProductStatus.Stocking:
                return query.Include(x => x.Category).Where(x => x.Status == ProductStatus.Stocking).ToListAsync();
            case ProductStatus.OutOfStock:
                return query.Include(x => x.Category).Where(x => x.Status == ProductStatus.OutOfStock).ToListAsync();
            default:
                return query.Include(x => x.Category).ToListAsync();
        }
    }

    public override Task<Product?> GetByIdAsync(int id, bool disableTracking = false)
    {
        IQueryable<Product> query = DbSet.AsNoTracking();
        return query.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id && x.Status != ProductStatus.Hide);
    }
}