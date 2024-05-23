using ClassLibrary1.Dtos.RequestDto.Account;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Interface.IRepositories;

public interface ICustomerRepository:IBaseRepository<Customer>
{
    bool CheckEmailExist(string email);
    Customer? VerifyEmailCode(string email, int code);
    Task<Customer?> LoginAsync(LoginRequestDto dto, LoginType type);
}