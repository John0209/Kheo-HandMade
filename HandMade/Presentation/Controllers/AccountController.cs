using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Dtos.ResponseDto;
using ClassLibrary1.Dtos.ResponseDto.Customer;
using ClassLibrary1.Interface.IServices;
using DataAccess.Enum;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/v1/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAccount(RegisterRequestDto dto)
    {
        await _accountService.RegisterCustomer(dto);
        return Ok(new
        {
            Message = "Register successful, please verify email to active your account"
        });
    }

    /// <summary>
    ///  Nhập code đã được gửi trong email để xác thực tài khoản
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("verify")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyEmail(VerifyRequestDto dto)
    {
        await _accountService.VerifyEmailCode(dto);
        return Ok(new
        {
            Message = "Verification successful"
        });
    }

    /// <summary>
    /// Login to the website
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <response code="201">Return account information</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponseDto>> LoginToWebsite(LoginRequestDto dto)
    {
        var result = await _accountService.LoginAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// Nhập email vào để khôi phục lại password
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpPost("recover")]
    public async Task<IActionResult> Recover(string email)
    {
        await _accountService.RecoverPassword(email);

        return Ok(new
        {
            Message = "New password has been sent to email " + email
        });
    }

    /// <summary>
    /// Thay đổi user password
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch("change-pass")]
    public async Task<IActionResult> ChangePass(PassChangeRequest request)
    {
        await _accountService.ChangePassword(request);

        return Ok(new
        {
            Message = "Change password successful"
        });
    }
}