using Application.ErrorHandlers;
using ClassLibrary1.Dtos.RequestDto.Firebase;
using ClassLibrary1.Interface;
using DataAccess.Enum;
using Firebase.Auth;
using Firebase.Storage;
using Kitchen.Application.Gateway.IConfiguration;
using Microsoft.AspNetCore.Http;

namespace ClassLibrary1.Third_Parties.Service;

public class FireBaseService : IFirebaseService
{
    private IUnitOfWork _unit;
    private IFireBaseConfig _configuration;

    public FireBaseService(IFireBaseConfig configuration, IUnitOfWork unit)
    {
        _configuration = configuration;
        _unit = unit;
    }

    private static readonly List<string> SupportedImageFile = new()
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".svg",
        ".webp",
        ".heic",
        ".heif",
        ".ico",
        ".gif"
    };

    private async Task<FirebaseAuthLink> GetAuthentication()
    {
        var auth = new FirebaseAuthProvider(new FirebaseConfig(_configuration.ApiKey));
        return await auth.SignInWithEmailAndPasswordAsync(_configuration.AuthEmail,
            _configuration.AuthPassword);
    }

    private async Task<string?> UploadImage(IFormFile file, string? fileName)
    {
        try
        {
            var stream = file.OpenReadStream();

            var fileExtension = Path.GetExtension(file.FileName);

            //Check file extension for preventing malware
            if (!SupportedImageFile.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
            {
                throw new BadRequestException("Unsupported file type.");
            }

            fileName += fileExtension;

            var firebaseAuthLink = await GetAuthentication();

            var task = new FirebaseStorage(_configuration.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(firebaseAuthLink.FirebaseToken),
                        ThrowOnCancel =
                            true // when you cancel the upload, exception is thrown. By default no exception is thrown
                    })
                .Child(fileName)
                .PutAsync(stream);

            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            return await task;
        }
        catch (Exception e)
        {
            throw new NotImplementException("Upload file to drive is failed, Details: " + e);
        }
    }
    
    public async Task UploadHandle(UploadFileRequest request)
    {
        string fileName = request.UploadType + "-" + request.Id;
        var fileUrl = await UploadImage(request.File, fileName);

        switch (request.UploadType)
        {
            case UploadType.Customer:
                var user = await _unit.CustomerRepository.GetByIdAsync(request.Id) ??
                           throw new NotFoundException("UserId not found");
                user.Picture = fileUrl;
                _unit.CustomerRepository.Update(user);
                break;
            case UploadType.Seller:
                var seller = await _unit.SellerRepository.GetByIdAsync(request.Id) ??
                             throw new NotFoundException("SellerId not found");
                seller.Avarta = fileUrl;
                _unit.SellerRepository.Update(seller);
                break;
            case UploadType.Product:
                var product = await _unit.ProductRepository.GetByIdAsync(request.Id) ??
                           throw new NotFoundException("ProductId not found");
                product.Picture = fileUrl;
                _unit.ProductRepository.Update(product);
                break;
        }

        if (await _unit.SaveChangeAsync() < 0) throw new NotImplementException("Update file to DB failed");
    }
    
    public async Task<string?> GetImage(string? fileName)
    {
        try
        {
            var firebaseAuthLink = await GetAuthentication();

            var task = new FirebaseStorage(_configuration.Bucket, new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(firebaseAuthLink.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child(fileName)
                .GetDownloadUrlAsync();

            return await task;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}