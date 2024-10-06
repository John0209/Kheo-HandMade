using System.ComponentModel.DataAnnotations;
using Application.Validations;

namespace ClassLibrary1.Dtos.RequestDto.Account;

public class LoginRequestDto
{
    [DataType(DataType.EmailAddress)]
    public string?  Email { get; set; }
    
    public required string Password { get; set; }
}