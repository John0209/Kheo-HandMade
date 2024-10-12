using ClassLibrary1.Dtos.RequestDto.Account;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Interface.IRepositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> LoginAsync(LoginRequestDto dto);
    public (User?, bool) CheckEmailExist(string email);
    public User? VerifyEmailCode(string email, int code);
}