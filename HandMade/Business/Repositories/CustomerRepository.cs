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
}