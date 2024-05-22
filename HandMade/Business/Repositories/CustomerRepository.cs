using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class CustomerRepository:BaseRepository<Customer>,ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context)
    {
    }

    public bool CheckEmailExist(string email)
    {
        var result = DbSet.FirstOrDefault(x => x.Email == email);
        return result != null;
    }

    public Customer? VerifyEmailCode(string email, int code)
    {
        return DbSet.FirstOrDefault(x => x.Email == email && x.TokenCode == code);
    }
}