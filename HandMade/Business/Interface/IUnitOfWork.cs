using ClassLibrary1.Interface.IRepositories;

namespace ClassLibrary1.Interface;

public interface IUnitOfWork:IDisposable
{
    ICustomerRepository CustomerRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderDetailRepository OrderDetailRepository { get; }
    ISellerRepository SellerRepository { get; }
    IUserRepository UserRepository { get; }
    public Task<int> SaveChangeAsync();
    
}