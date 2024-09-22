using Application.Utils;
using ClassLibrary1.Dtos.ResponseDto.Order;
using DataAccess.Entites;
using Microsoft.VisualBasic;

namespace ClassLibrary1.Mapper;

public static class OrderMapper
{
    public static List<OrderResponse> OrdersToOrdersResponse(List<Order> dto)
    {
     
       return dto.Select(x => new OrderResponse()
        {
            Id = x.Id,
            OrderCode = x.OrderCode,
            Status = x.OrderStatus,
            QuantityTotal = x.Quantity,
            PriceTotal = x.Total,
            OrderDetails = ConvertToListOrderDetailsDto(x.OrderDetails.ToList())
        }).ToList();
    }
    private static List<OrderDetailsDto> ConvertToListOrderDetailsDto(List<OrderDetail> dto)
    {
       return dto.Select(x => new OrderDetailsDto()
        {
            ProductName = x.Product!.ProductName,
            OrderQuantity = x.Quantity,
            ProductPrice = x.Product!.Price,
            ProductImage = x.Product!.Picture
        }).ToList();
    }

    public static OrderDetailsResponse OrdersToOrderDetailResponse(Order x) => new OrderDetailsResponse()
    {
        Id = x.Id,
        OrderCode = x.OrderCode,
        Status = x.OrderStatus,
        OrderDate =DateUtils.FormatDateTimeToDatetimeV1(x.OrderDate),
        PaymentType = x.PaymentType,
        QuantityTotal = x.Quantity,
        PriceTotal = x.Total,
        CustomerName = x.Customer?.FullName,
        OrderDetails = ConvertToListOrderDetailsDto(x.OrderDetails.ToList())
    };

    public static OrderAdminResponse OrderToOrderAdminResponse(List<Order> dto) => new OrderAdminResponse()
    {
        TotalOrder = dto.Count,
        Orders = ConvertToListOrderAdminDto(dto)
    };

    private static List<OrderAdminDto> ConvertToListOrderAdminDto(List<Order> dto)
    {
        return dto.Select(x => new OrderAdminDto()
        {
            Id = x.Id,
            OrderCode = x.OrderCode,
            Total = x.Total,
            Status = x.OrderStatus,
            CustomerName = x.Customer!.FullName
        }).ToList();
    }
}
