using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public Task<User?> LoginAsync(LoginRequestDto dto)
    {
        return DbSet.Include(x => x.Role).Include(x => x.Seller)
            .Include(x => x.Customer).FirstOrDefaultAsync(x =>
                x.Email == dto.Email && x.PassWord == dto.Password);
    }

    public (User?, bool) CheckEmailExist(string email)
    {
        return (DbSet.FirstOrDefault(x => x.Email == email), DbSet.Any(x => x.Email == email));
    }


    public User? VerifyEmailCode(string email, int code)
    {
        return DbSet.Include(x => x.Customer).FirstOrDefault(x => x.Customer != null && x.Email == email && x.Customer.TokenCode == code);
    }
    
}