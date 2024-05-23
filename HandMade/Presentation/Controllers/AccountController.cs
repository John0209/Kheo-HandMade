using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Dtos.ResponseDto;
using ClassLibrary1.Interface.IServices;
using DataAccess.Enum;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Controllers;

[ApiController]
[Route("api/v1/account")]
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
   /// <param name="type">1.Login By Email, 2.Login By Phone</param>
   /// <returns></returns>
   [HttpPost("login")]
   [ProducesResponseType(StatusCodes.Status201Created)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<LoginResponseDto>> LoginToWebsite(LoginRequestDto dto, LoginType type)
   {
      var result = await _accountService.LoginAsync(dto, type);
      return Ok(result);
   }
}