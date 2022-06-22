using ECom.DataAccess.Data;
using ECom.Models;
using EComWeb.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace EComWeb.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task CreateOrderAsync(int basketId, Address shippingAddress)
    {
        var basket = await _context.Baskets.Include(b => b.Items).FirstOrDefaultAsync(b => b.Id == basketId);

        var productIds = basket.Items.Select(i => i.Id);
        var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

        var items = basket.Items.Select(b =>
        {
            var product = products.First(p => p.Id == b.Id);
            var itemOrdered = new
            {
                ImageUrl = product.ImageUrl,
                ProductName = product.Name
            };
            var orderItem = new OrderItem
            {
                ProductName = itemOrdered.ProductName,
                ImageUrl = itemOrdered.ImageUrl,
                UnitPrice = b.UnitPrice,
                Units = b.Quantity,
                ProductId = product.Id
            };
            return orderItem;
        }).ToList();
        var currentOrderStatus = new OrderStatus
        {
            Name = OrderStatus.OrderStatusType.Shipping,
            StartDate = DateTime.Now
        };
        var order = new Order
        {
            BuyerId = basket.BuyerId,
            OrderItems = items,
            ShipToAddress = shippingAddress
        };
        order.OrderStatuses.Add(currentOrderStatus);
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateOrderStatusAsync(int orderId, string orderStatus)
    {
        if (!Enum.IsDefined(typeof(OrderStatus.OrderStatusType), orderStatus))
        {
            throw new ArgumentException(String.Format("Wrong OrderStatus: {orderStatus}"));
        }
        var order = await _context.Orders.Include(o=>o.OrderStatuses).FirstOrDefaultAsync(o=>o.Id==orderId);
        var currentDate = DateTime.Now;
        order.OrderStatuses.Last().EndDate=currentDate;
        order.OrderStatuses.Add(new ECom.Models.OrderStatus
        {
            Name = (OrderStatus.OrderStatusType)Enum.Parse<OrderStatus.OrderStatusType>(orderStatus),
            StartDate = currentDate
        });
    }
}