using Application.ErrorHandlers;
using ClassLibrary1.Interface;
using ClassLibrary1.Interface.IServices;
using ClassLibrary1.Third_Parties.Service;
using DataAccess.Entites;
using DataAccess.Enum;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace ClassLibrary1.Service;

public class ExcelService : IExcelService
{
    private readonly IUnitOfWork _unit;
    private IFirebaseService _firebase;

    public ExcelService(IUnitOfWork unit, IFirebaseService firebase)
    {
        _unit = unit;
        _firebase = firebase;
    }

    public async Task ImportExcel(IFormFile file)
    {
        // Đảm bảo rằng EPPlus có thể sử dụng
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Lấy worksheet đầu tiên
                var rowCount = worksheet.Dimension.Rows; // Số lượng hàng

                int number = 1;
                // Khởi tạo list recipes để add vào database
                var product = new List<Product>();
                // bool isLineEmpty = false;
                for (int row = 7; row <= rowCount; row++)
                {
                    var productDto = new Product()
                    {
                        Status = ProductStatus.Stocking,
                        Picture = await GetImageFromFirebase(number, UploadType.Product)
                    };

                    for (int col = 2; col <= 7; col++)
                    {
                        var cellTitle = worksheet.Cells[6, col].Text.Trim(); // Lấy giá trị title
                        var cellValue = worksheet.Cells[row, col].Text.Trim(); // Lấy giá trị ô

                        if (string.IsNullOrWhiteSpace(cellValue))
                        {
                            goto AddProduct;
                        }

                        ExcelToProductDto(cellTitle, cellValue, ref productDto);
                    }

                    number++;
                    product.Add(productDto);
                }

                AddProduct:
                await _unit.ProductRepository.AddRangeAsync(product);
                if (await _unit.SaveChangeAsync() < 0) throw new NotImplementException("Add list product to DB failed");
            }
        }
    }

    private void ExcelToProductDto(string title, string value, ref Product productDto)
    {
        Enum.TryParse<ExcelTitle>(title, out var excelTitle);

        switch (excelTitle)
        {
            case ExcelTitle.ProductName:
                productDto.ProductName = value;
                break;
            case ExcelTitle.Description:
                productDto.Description = value;
                break;
            case ExcelTitle.Price:
                productDto.Price = decimal.Parse(value);
                break;
            case ExcelTitle.Quantity:
                productDto.Quantity = Int16.Parse(value);
                break;
            case ExcelTitle.CategoryId:
                productDto.CategoryId = Int16.Parse(value);
                break;
            case ExcelTitle.SellerId:
                productDto.SellerId = Int16.Parse(value);
                break;
        }
    }

    private async Task<string?> GetImageFromFirebase(int id, UploadType type)
    {
        string fileName = type + "-" + id + ".jpg";
        return await _firebase.GetImage(fileName);
    }
}