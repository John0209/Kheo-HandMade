using ClassLibrary1.Dtos.ResponseDto.User;
using ClassLibrary1.Interface.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    /// <summary>
    /// Lấy thông tin chi tiết của customer và seller
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserDetailResponse>> GetDetail(int id)
    {
        var result = await _service.GetUserDetail(id);
        return Ok(result);
    }
}