using Microsoft.AspNetCore.Http;

namespace ClassLibrary1.Third_Parties.Service;

public interface ICloudService
{
    public Task<string?> UploadImage(IFormFile file, int id);
    public string GetLinkImage(int id);
}