using ECom.Models;

namespace EComWeb.ViewModels;

public class OrderViewModel
{
    public OrderViewModel(Order order)
    {
        Id=order.Id;
        OrderStatus = order.OrderStatus.Name;
        decimal total = 0;
        foreach(var item in order.OrderItems)
        {
            total += item.Units * item.UnitPrice;
        }
        TotalPrice = total;
        ShippingAddress = order.ShipToAddress;
        OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
        {
            Id = oi.Id,
            ProductName = oi.ProductName,
            UnitPrice = oi.UnitPrice,
            Units = oi.Units,
            ImageUrl = oi.ImageUrl
        }).ToList();
    }
    public int Id { get; set; }
    public string OrderStatus { get; set; }
    public decimal TotalPrice { get; set; }
    public Address ShippingAddress { get; set; }
    public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
}