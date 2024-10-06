using ClassLibrary1.Dtos.ResponseDto.Customer;
using DataAccess.Entites;

namespace ClassLibrary1.Mapper;

public static class UserMapper
{
    public static LoginResponseDto UserToLoginResponse(User dto) => new LoginResponseDto()
    {
        Id = dto.Id,
        Name = dto.FullName,
        Email = dto.Email,
        Role = dto.Role!.RoleType
    };
}