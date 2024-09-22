using DataAccess.Enum;
using Microsoft.AspNetCore.Http;

namespace ClassLibrary1.Dtos.RequestDto.Drive;

public class UploadFileRequestDto
{
    public int Id { get; set; }
    public required IFormFile File { get; set; }
    public UploadType UploadType { get; set; }
    public ContentType ContentType { get; set; }
}