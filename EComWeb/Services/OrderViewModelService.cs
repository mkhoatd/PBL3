﻿using ECom.DataAccess.Data;
using EComWeb.Interfaces;
using EComWeb.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EComWeb.Services;

public class OrderViewModelService : IOrderViewModelService
{
    private readonly ApplicationDbContext _context;
    public OrderViewModelService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<OrderViewModel>> GetAllOrderAsync(int userId)
    {
        var orders=await _context.Orders.Where(o=>o.BuyerId == userId).Include(o=>o.OrderStatuses).ToListAsync();
        var results=new List<OrderViewModel>();
        foreach(var order in orders)
        {
            results.Add(new OrderViewModel(order));
        }
        return results;
    }

    public async Task<OrderViewModel> GetOrderDetailAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        return new OrderViewModel(order);

    }
}