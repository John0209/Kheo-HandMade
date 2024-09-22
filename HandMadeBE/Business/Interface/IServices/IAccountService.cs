using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Dtos.ResponseDto;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Interface.IServices;

public interface IAccountService
{
    Task RegisterCustomer(RegisterRequestDto dto);
    Task VerifyEmailCode(VerifyRequestDto dto);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto dto, LoginType type);
    public Task RecoverPassword(string email);
    public Task ChangPassword(PassChangeRequestDto request);
}