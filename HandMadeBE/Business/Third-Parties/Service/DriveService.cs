using Application.ErrorHandlers;
using DataAccess.Enum;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ClassLibrary1.Third_Parties.Service;

public class DriveService : IDriveService
{
    private IDriveConfig _drive;

    //khởi tạo phương thức google drive
    GoogleCredential _credential;
    private Google.Apis.Drive.v3.DriveService _service;
    private const string FolderId = "1PXIxwBczudrm24NANpmTam5zcnSyCSDq";

    public DriveService(IDriveConfig cloud)
    {
        _drive = cloud;
    }

    private void InitializeGgDrive()
    {
        string jsonDrive = JsonConvert.SerializeObject(_drive);

        _credential = GoogleCredential.FromJson(jsonDrive).CreateScoped(new[]
        {
            Google.Apis.Drive.v3.DriveService.ScopeConstants.DriveFile
        });

        _service = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = _credential,
            ApplicationName = "Google Drive Upload Console App"
        });
    }

    public async Task<string?> UploadFileToGoogleDrive(IFormFile fileVideo, string? fileName, ContentType type)
    {
        try
        {
            InitializeGgDrive();
            // Tạo một luồng từ tệp đã tải lên
            using var stream = new MemoryStream();
            await fileVideo.CopyToAsync(stream);
            stream.Position = 0; // Đặt lại vị trí của con trỏ luồng

            // Tạo metadata cho file
            var fileMetadataVideo = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string> { FolderId }
            };

            string contentType = "";
            // Tạo yêu cầu tải file lên Google Drive
            switch (type)
            {
                case ContentType.Video:
                    contentType = "video/*";
                    break;
                case ContentType.Image:
                    contentType = "image/*";
                    break;
            }

            var requestVideo = _service.Files.Create
                (fileMetadataVideo, stream, contentType);
            requestVideo.Fields = "id";
            var response = await requestVideo.UploadAsync();

            if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
            {
                throw new NotImplementException("Upload file to drive is failed");
            }

            // Lấy ID của file đã tải lên
            var fileId = requestVideo.ResponseBody?.Id;

            // Cập nhật quyền chia sẻ file để công khai
            var permission = new Google.Apis.Drive.v3.Data.Permission()
            {
                Type = "anyone",
                Role = "reader"
            };
            await _service.Permissions.Create(permission, fileId).ExecuteAsync();
            // Tạo link preview
            return "https://drive.google.com/file/d/" + fileId + "/preview";
        }
        catch (Exception e)
        {
            throw new NotImplementException("Upload file to drive is failed, Details: " + e);
        }
    }
}