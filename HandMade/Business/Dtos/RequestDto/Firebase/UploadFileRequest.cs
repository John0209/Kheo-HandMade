using DataAccess.Enum;
using Microsoft.AspNetCore.Http;

namespace ClassLibrary1.Dtos.RequestDto.Firebase;

public class UploadFileRequest 
{
    public int Id { get; set; }
    public required IFormFile File { get; set; }
    public UploadType UploadType { get; set; }
}