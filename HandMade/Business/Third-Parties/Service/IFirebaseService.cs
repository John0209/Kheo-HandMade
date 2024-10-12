using ClassLibrary1.Dtos.RequestDto.Firebase;

namespace ClassLibrary1.Third_Parties.Service;

public interface IFirebaseService
{
    public Task UploadHandle(UploadFileRequest request);
    public Task<string?> GetImage(string? fileName);
}