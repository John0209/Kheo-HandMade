using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using DataAccess.Enum;
using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context)
    {
    }

    public Customer? VerifyEmailCode(string email, int code)
    {
        return DbSet.FirstOrDefault(x => x.Email == email && x.TokenCode == code);
    }

    public (Customer?, bool) CheckEmailExist(string email)
    {
        return (DbSet.FirstOrDefault(x => x.Email == email), DbSet.Any(x => x.Email == email));
    }

    public Task<Customer?>? LoginAsync(LoginRequestDto dto, LoginType type)
    {
        switch (type)
        {
            case LoginType.LoginByEmail:
                return DbSet.FirstOrDefaultAsync(x =>
                    x.Email == dto.Email && x.PassWord == dto.Password && x.Status == UserStatus.Actived);
            case LoginType.LoginByPhone:
                return DbSet.FirstOrDefaultAsync(x =>
                    x.PhoneNumber == dto.PhoneNumber && x.PassWord == dto.Password && x.Status == UserStatus.Actived);
            default:
                return null;
        }
    }
}