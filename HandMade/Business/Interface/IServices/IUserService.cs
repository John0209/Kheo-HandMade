using ClassLibrary1.Dtos.ResponseDto.User;

namespace ClassLibrary1.Interface.IServices;

public interface IUserService
{
    public Task<GetUserDetailResponse> GetUserDetail(int id);
}