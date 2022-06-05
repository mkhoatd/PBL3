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
            var itemOrdered = new ItemOrdered
            {
                ItemId = product.Id,
                ImageUrl = product.ImageUrl,
                ProductName = product.Name
            };
            var orderItem = new OrderItem
            {
                ItemOrdered = itemOrdered,
                UnitPrice = b.UnitPrice,
                Units = b.Quantity
            };
            return orderItem;
        }).ToList();
        var order = new Order
        {
            BuyerId = basket.BuyerId,
            OrderItems = items,
            OrderStatusId = await _context.OrderStatuses.Where(os=>os.Name=="Đang giao").Select(os=>os.Id).FirstOrDefaultAsync(),
            ShipToAddress = shippingAddress
        };
        _context.Orders.Add(order);
    }
}