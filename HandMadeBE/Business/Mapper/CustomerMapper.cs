using ClassLibrary1.Dtos.ResponseDto;
using DataAccess.Entites;

namespace ClassLibrary1.Mapper;

public static class CustomerMapper
{
    public static LoginResponseDto CustomerToLoginResponseDto(Customer dto) => new LoginResponseDto()
    {
        CustomerId = dto.Id,
        Name = dto.FullName,
        PhoneNumber = dto.PhoneNumber,
        Email = dto.Email
    };
}