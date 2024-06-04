using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ClassLibrary1.Third_Parties.Service;

public class CloudService : ICloudService
{
    private ICloudConfig _cloud;
    private GoogleCredential _credential;
    private StorageClient _storage;
    private readonly string _bucketName = "handmade01";
    private readonly string _imageName = "Image_";

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

    public async Task<string?> UploadImage(IFormFile file, int id)
    {
        InitializeCloud();
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;

        var type = "image/png";
        var obj = new Google.Apis.Storage.v1.Data.Object
        {
            Bucket = _bucketName,
            Name = _imageName + id,
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
        await _storage.UploadObjectAsync(obj, stream);

        return GetLinkImage(id);
    }

    public string GetLinkImage(int id)
    {
        InitializeCloud();
        UrlSigner url = UrlSigner.FromCredential(_credential);
        return url.Sign(_bucketName, _imageName + id, TimeSpan.FromHours(1), HttpMethod.Get);
    }
}