
using DataAccess.Enum;
using Microsoft.AspNetCore.Http;

namespace ClassLibrary1.Third_Parties.Service;

public interface IDriveService
{
    public Task<string?> UploadFileToGoogleDrive(IFormFile fileVideo, string? fileName, ContentType type);
}