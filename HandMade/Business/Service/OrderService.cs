using System.Text.RegularExpressions;
using Application.ErrorHandlers;
using Application.Utils;
using ClassLibrary1.Dtos.RequestDto.Order;
using ClassLibrary1.Interface;
using ClassLibrary1.Interface.IServices;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Service;

public class OrderService : IOrderService
{
    private IUnitOfWork _unit;

    public OrderService(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<int> CreateOrderAsync(OrderCreationRequestDto dto)
    {
        var order = new Order()
        {
            OrderCode = Int32.Parse(StringUtils.GenerateRandomNumber(4)),
            OrderDate = DateTime.Now,
            OrderStatus =(int) OrderStatus.Processing,
            CustomerId = dto.CustomerId,
            Quantity = dto.Quantity,
            Total = dto.Total
        };
        
        var orderDetails = new List<OrderDetail>();
        foreach (var x in dto.Products)
        {
            var detail = new OrderDetail()
            {
                Quantity = x.ProductQuantity,
                ProductId = x.ProductId,
                Price = x.ProductPrice,
                Order = order
            };
            orderDetails.Add(detail);
        }

        await _unit.OrderDetailRepository.AddRangeAsync(orderDetails);
        var result= await _unit.SaveChangeAsync();
        if (result < 1)
        {
            throw new NotImplementException("Create order failed");
        }
        return order.Id;
    }

    public Order? GetOrderByProcessing(int id)
    {
        var order = _unit.OrderRepository.GetOrderByProcessing(id) ??
                    throw new NotFoundException("OrderId " + id + " not found");
        return order;
    }

    public async Task UpdateOrderStatus(int id, OrderStatus status)
    {
        var order =await _unit.OrderRepository.GetByIdAsync(id)??
                   throw new NotFoundException("OrderId "+id+" not found");
        order.OrderStatus = status;
        _unit.OrderRepository.Update(order);
        var result =await _unit.SaveChangeAsync();
        if (result <= 0)
            throw new NotImplementException("Update order status failed");
    }
    public int GetIdMomoResponse(string id)
    {
        Regex regex = new Regex("-(\\d+)");
        var macth = regex.Match(id);
        if (macth.Success) return Int32.Parse(macth.Groups[1].Value);
        return 0;
    }
}