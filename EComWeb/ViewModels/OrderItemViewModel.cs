﻿namespace EComWeb.ViewModels;

public class OrderItemViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
    public string ImageUrl { get; set; }
}