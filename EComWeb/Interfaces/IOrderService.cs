﻿using ECom.DataAccess.Data;
using ECom.Models;

namespace EComWeb.Interfaces;

public interface IOrderService
{
    
    Task CreateOrderAsync(int basketId, Address shippingAddress);
}