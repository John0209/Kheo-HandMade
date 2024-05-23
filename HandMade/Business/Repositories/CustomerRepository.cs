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

    public bool CheckEmailExist(string email)
    {
        var result = DbSet.FirstOrDefault(x => x.Email == email);
        return result != null;
    }

    public Customer? VerifyEmailCode(string email, int code)
    {
        return DbSet.FirstOrDefault(x => x.Email == email && x.TokenCode == code);
    }

    public async Task<Customer?> LoginAsync(LoginRequestDto dto, LoginType type)
    {
        switch (type)
        {
            case LoginType.LoginByEmail:
                return await DbSet.FirstOrDefaultAsync(x =>
                    x.Email == dto.Email && x.PassWord == dto.Password && x.Status == UserStatus.Actived);
            case LoginType.LoginByPhone:
                return await DbSet.FirstOrDefaultAsync(x =>
                    x.PhoneNumber == dto.PhoneNumber && x.PassWord == dto.Password && x.Status == UserStatus.Actived);
            default:
                return null;
        }
    }
}