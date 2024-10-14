using System.Text.RegularExpressions;
using Application.ErrorHandlers;
using Application.Utils;
using ClassLibrary1.Dtos.RequestDto.Order;
using ClassLibrary1.Dtos.ResponseDto.Order;
using ClassLibrary1.Interface;
using ClassLibrary1.Interface.IServices;
using ClassLibrary1.Mapper;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Service;

public class OrderService : IOrderService
{
    private IUnitOfWork _unit;
    private IProductService _product;

    public OrderService(IUnitOfWork unit, IProductService product)
    {
        _unit = unit;
        _product = product;
    }

    public async Task<int> CreateOrderAsync(OrderCreationRequestDto dto, PaymentType type)
    {
        var customer = await _unit.CustomerRepository.GetCustomerByUserId(dto.CustomerId) ??
                       throw new NotFoundException("UserId not found");

        var order = new Order()
        {
            OrderCode = Int32.Parse(StringUtils.GenerateRandomNumber(8)),
            OrderDate = DateTime.Now,
            CustomerId = customer!.Id,
            Quantity = dto.Quantity,
            Total = dto.Total,
            PaymentType = type
        };

        switch (type)
        {
            case PaymentType.Momo:
                order.OrderStatus = OrderStatus.Processing;
                break;
            case PaymentType.Cash:
                order.OrderStatus = OrderStatus.Confirming;
                break;
        }

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
        var result = await _unit.SaveChangeAsync();
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

    /// <summary>
    /// Update và cập nhật số tiền cho seller nêu success
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="NotImplementException"></exception>
    public async Task UpdateOrderStatus(int id, OrderStatus status)
    {
        var order = await _unit.OrderRepository.GetByIdAsync(id) ??
                    throw new NotFoundException("OrderId " + id + " not found");

        if (status == OrderStatus.Success)
        {
            foreach (var detail in order.OrderDetails)
            {
                detail.Product!.Seller!.Wallet += (detail.Quantity * detail.Price);
            }

            order.OrderStatus = status;
        }
        else
        {
            order.OrderStatus = status;
        }

        _unit.OrderRepository.Update(order);
        var result = await _unit.SaveChangeAsync();
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

    public async Task<int> CreateOrderTestAsync(OrderCreationRequestDto dto)
    {
        var orderDetails = new List<OrderDetail>();
        foreach (var x in dto.Products)
        {
            var detail = new OrderDetail()
            {
                Quantity = x.ProductQuantity,
                ProductId = x.ProductId,
                Price = x.ProductPrice,
                OrderId = 29
            };
            orderDetails.Add(detail);
        }

        await _unit.OrderDetailRepository.AddRangeAsync(orderDetails);
        var result = await _unit.SaveChangeAsync();
        if (result < 1)
        {
            throw new NotImplementException("Create order failed");
        }

        return 3;
    }

    public async Task<List<OrderResponse>> GetOrders(OrderStatus? status, int userId)
    {
        var orders = await _unit.OrderRepository.GetOrdersByStatus(status, userId);
        var orderDetails = orders.SelectMany(x => x.OrderDetails);

        return OrderMapper.OrdersToOrdersResponse(orders);
    }

    public async Task<OrderDetailsResponse> GetOrderDetails(int id)
    {
        var order = await _unit.OrderRepository.GetByIdAsync(id) ??
                    throw new NotFoundException("OrderId " + id + " not found!");

        return OrderMapper.OrdersToOrderDetailResponse(order);
    }

    public async Task<OrderAdminResponse> AdminGetOrders(OrderStatus status)
    {
        var orders = await _unit.OrderRepository.AdminGetOrder(status);
        return OrderMapper.OrderToOrderAdminResponse(orders);
    }
}