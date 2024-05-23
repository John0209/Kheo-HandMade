using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Interface.IRepositories;

public interface IProductRepository:IBaseRepository<Product>
{
    Task<List<Product>> GetProductsAsync(ProductStatus status);
}