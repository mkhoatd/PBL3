using ECom.DataAccess.Data;
using ECom.Models;
using EComWeb.Interfaces;
using Microsoft.EntityFrameworkCore;

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
}