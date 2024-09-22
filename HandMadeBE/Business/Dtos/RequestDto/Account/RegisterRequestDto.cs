using System.ComponentModel.DataAnnotations;
using Application.Validations;

namespace ClassLibrary1.Dtos.RequestDto.Account;

public class RegisterRequestDto
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    //[PasswordValidation]
    public required string Password { get; set; }
    [Required]public required string Name { get; set; }
    [DataType(DataType.Date)] public DateTime BirthDate { get; set; }
    [PhoneValidation] public string? PhoneNumber { get; set; }
   
}