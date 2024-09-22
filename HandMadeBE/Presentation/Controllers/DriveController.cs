using Application.ErrorHandlers;
using ClassLibrary1.Dtos.RequestDto.Drive;
using ClassLibrary1.Interface.IServices;
using DataAccess.Enum;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/v1/drives")]
public class DriveController : ControllerBase
{
    private IProductService _product;

    public DriveController(IProductService product)
    {
        _product = product;
    }

    [HttpPatch("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file, ContentType contentType, UploadType uploadType, int id)
    {
        if (file == null) throw new BadRequestException("File is empty");

        var dto = new UploadFileRequestDto()
        {
            File = file,
            ContentType = contentType,
            UploadType = uploadType,
            Id = id
        };

        await _product.UploadFile(dto);

        return Ok(new
        {
            Message = "Upload file successful"
        });
    }
}