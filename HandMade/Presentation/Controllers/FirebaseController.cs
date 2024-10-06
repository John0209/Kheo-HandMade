using Application.ErrorHandlers;
using ClassLibrary1.Dtos.RequestDto.Firebase;
using ClassLibrary1.Third_Parties.Service;
using DataAccess.Enum;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/v1/firebase")]
public class FirebaseController : ControllerBase
{
    private IFirebaseService _firebase;

    public FirebaseController(IFirebaseService firebase)
    {
        _firebase = firebase;
    }

    /// <summary>
    /// Upload image cho customer, seller và product
    /// </summary>
    /// <param name="file"></param>
    /// <param name="uploadType"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BadRequestException"></exception>
    [HttpPatch("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file, UploadType uploadType, int id)
    {
        if (file == null) throw new BadRequestException("File is empty");

        var dto = new UploadFileRequest()
        {
            File = file,
            UploadType = uploadType,
            Id = id
        };

        await _firebase.UploadHandle(dto);

        return Ok(new
        {
            Message = "Upload file successful"
        });
    }
}