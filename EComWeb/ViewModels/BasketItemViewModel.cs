using System.ComponentModel.DataAnnotations;

namespace EComWeb.ViewModels;

public class BasketItemViewModel
{
    public int Id { get; set; }
    public int ProductItemId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be bigger than 0")]
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
}