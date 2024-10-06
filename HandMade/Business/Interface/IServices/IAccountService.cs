using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Dtos.ResponseDto;
using ClassLibrary1.Dtos.ResponseDto.Customer;
using ClassLibrary1.Dtos.ResponseDto.Seller;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Interface.IServices;

public interface IAccountService
{
    Task RegisterCustomer(RegisterRequestDto dto);
    Task VerifyEmailCode(VerifyRequestDto dto);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    Task RecoverPassword(string email);
    Task ChangePassword(PassChangeRequest request);
}