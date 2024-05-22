using Application.ErrorHandlers;
using ClassLibrary1.Interface;
using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class UnitOfWork:IUnitOfWork
{
    public UnitOfWork(ICustomerRepository customerRepository, ICategoryRepository categoryRepository, IProductRepository productRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
    {
        CustomerRepository = customerRepository;
        CategoryRepository = categoryRepository;
        ProductRepository = productRepository;
        OrderRepository = orderRepository;
        OrderDetailRepository = orderDetailRepository;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
    private readonly AppDbContext _context;
    public ICustomerRepository CustomerRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IOrderDetailRepository OrderDetailRepository { get; }
    public async Task<int> SaveChangeAsync()
    {
        //Handle concurrency update
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                var databaseValues = await entry.GetDatabaseValuesAsync();

                if (databaseValues != null)
                {
                    // Refresh original values to bypass next concurrency check
                    entry.OriginalValues.SetValues(databaseValues);
                }
                else
                {
                    // Handle entity not found in the database
                    throw new NotFoundException("Entity not found in the database.");
                }
            }

            // Try saving changes again after resolving conflicts
            return await _context.SaveChangesAsync();
        }
    }
}