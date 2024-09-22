using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Dtos.RequestDto.Account;

public class VerifyRequestDto
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public required int Code { get; set; }
}