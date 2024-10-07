using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using DataAccess.Enum;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context)
    {
    }

    public Task<Customer?> GetCustomerByUserId(int id)
    {
        return DbSet.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == id);
    }
}