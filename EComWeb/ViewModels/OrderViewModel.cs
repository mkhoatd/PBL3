using ECom.Models;

namespace EComWeb.ViewModels;

public class OrderViewModel
{
    public int Id { get; set; }
    public string OrderStatus { get; set; }
    public decimal Total { get; set; }
    public Address ShippingAddress { get; set; }
    public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
}