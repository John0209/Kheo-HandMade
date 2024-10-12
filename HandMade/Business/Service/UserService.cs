using Application.ErrorHandlers;
using ClassLibrary1.Dtos.ResponseDto.User;
using ClassLibrary1.Interface;
using ClassLibrary1.Interface.IServices;
using ClassLibrary1.Mapper;

namespace ClassLibrary1.Service;

public class UserService : IUserService
{
    IUnitOfWork _unit;

    public UserService(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<GetUserDetailResponse> GetUserDetail(int id)
    {
        var user = await _unit.UserRepository.GetByIdAsync(id) ?? throw new NotFoundException("UserId not found");
        return UserMapper.UserToGetUserDetailResponse(user);
    }
}