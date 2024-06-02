

using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ClassLibrary1.Third_Parties.Service;

public class CloudService:ICloudService
{
    private ICloudConfig _cloud;
    private GoogleCredential _credential;
    private StorageClient _storage;
    public CloudService(ICloudConfig cloud)
    {
        _cloud = cloud;
    }

    private void InitializeCloud()
    {
        string jsonCloud = JsonConvert.SerializeObject(_cloud);
        _credential = GoogleCredential.FromJson(jsonCloud).CreateScoped(new[]
        {
            StorageService.ScopeConstants.DevstorageFullControl
        });
        _storage = StorageClient.Create(_credential);
    }

    public async Task<string?> UploadImage(IFormFile file)
    {
        InitializeCloud();
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;

        var imageName = $"{file.FileName}";
        var bucketName = "handmade01";
        var type = "image/png";
        var obj = new Google.Apis.Storage.v1.Data.Object
        {
            Bucket = bucketName,
            Name = imageName,
            ContentType = type,
            Acl = new List<ObjectAccessControl>
            {
                new ObjectAccessControl
                {
                    Entity = "allUsers",
                    Role = "READER"
                }
            }
        };
        var result= await _storage.UploadObjectAsync(obj, stream);

        UrlSigner url = UrlSigner.FromCredential(_credential);
        return url.Sign(bucketName, imageName, TimeSpan.FromHours(1), HttpMethod.Get);
    }
    
}