using Application.Utils;
using ClassLibrary1.Dtos.ResponseDto.Authenticate;
using ClassLibrary1.Dtos.ResponseDto.User;
using DataAccess.Entites;
using DataAccess.Enum;

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

    public static GetUserDetailResponse UserToGetUserDetailResponse(User dto)
    {
        var response = new GetUserDetailResponse()
        {
            Id = dto.Id,
            Name = dto.FullName,
            Email = dto.Email,
        };

        switch (dto.RoleId)
        {
            case (int)RoleType.Customer:
                response.Role = RoleType.Customer;
                response.Address = dto.Customer!.Address;
                response.PhoneNumber = dto.Customer!.PhoneNumber;
                response.Avarta = dto.Customer!.Picture;
                response.BirthDate =DateUtils.FormatDateTimeToDateV1(dto.Customer!.BirthDate);
                break;
            case (int)RoleType.Seller:
                response.Role = RoleType.Seller;
                response.Wallet = dto.Seller!.Wallet;
                response.Avarta = dto.Seller.Avarta;
                break;
        }

        return response;
    }
}