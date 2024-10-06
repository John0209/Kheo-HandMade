using Microsoft.AspNetCore.Http;

namespace ClassLibrary1.Interface.IServices;

public interface IExcelService
{
    public Task ImportExcel(IFormFile file);
}