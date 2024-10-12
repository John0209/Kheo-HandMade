using Application.ErrorHandlers;
using ClassLibrary1.Interface.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/v1/excel")]
public class ExcelController : ControllerBase
{
    private IExcelService _service;

    public ExcelController(IExcelService service)
    {
        _service = service;
    }

    
    /// <summary>
    /// Import product data từ file excel, gửi file vào 
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost("import")]
    public async Task<IActionResult> Import(IFormFile file)
    {
        if (file.Length == 0)
        {
            throw new BadRequestException("File is empty");
        }

        await _service.ImportExcel(file);

        return Ok(new
        {
            Message = "Import information to product db successful"
        });
    }
}