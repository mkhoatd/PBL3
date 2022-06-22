using ECom.DataAccess.Data;
using ECom.Models;
using EComWeb.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EComWeb.Services;

public class BasketService : IBasketService
{
    private readonly ApplicationDbContext _context;

    public BasketService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Basket> AddItemToBasketAsync(int buyerId, int productItemId, int quantity)
    {
        var basket = await  _context.Baskets.Include(b=>b.Items).FirstOrDefaultAsync(b => b.BuyerId == buyerId);
        if (basket == null)
        {
            basket = new Basket()
            {
                BuyerId = buyerId
            };
            await _context.Baskets.AddAsync(basket);
        }
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productItemId);
        var unitPrice = product.Price * (1 - product.Discount);
        basket.AddItem(productItemId, unitPrice, quantity);
        await _context.SaveChangesAsync();
        return basket;
    }

    public async Task<Basket> SetQuantitesAsync(int basketId, Dictionary<string, int> quantities)
    {
        var basket = await _context.Baskets.Where(b => b.Id == basketId).Include(b => b.Items).FirstAsync();
        foreach (var item in basket.Items)
        {
            if (quantities.TryGetValue(item.Id.ToString(), out var quantity))
            {
                item.SetQuantity(quantity);
            }
        }
        basket.RemoveEmptyItems();
        await _context.SaveChangesAsync();
        return basket;
    }

    public async Task DeleteBasketAsync(int basketId)
    {
        var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.Id == basketId);
        _context.Remove(basket);
        await _context.SaveChangesAsync();
    }
}