using ClassLibrary1.Dtos.RequestDto.Account;

namespace ClassLibrary1.Interface.IServices;

public interface IAccountService
{
    Task RegisterCustomer(RegisterRequestDto dto);
    Task VerifyEmailCode(VerifyRequestDto dto);
}