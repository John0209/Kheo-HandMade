using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Interface.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Controllers;

[ApiController]
[Route("api/v1/account")]
public class AccountController : ControllerBase
{
   private IAccountService _accountService;

   public AccountController(IAccountService accountService)
   {
      _accountService = accountService;
   }

   [HttpPost]
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
   [HttpGet]
   [ProducesResponseType(StatusCodes.Status201Created)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> VerifyEmail(VerifyRequestDto dto)
   {
      await _accountService.VerifyEmailCode(dto);
      return Ok(new
      {
         Message = "Verify successful"
      });
   }
}