using EComWeb.ViewModels;

namespace EComWeb.Interfaces;

public interface IOrderViewModelService
{
    Task<OrderViewModel> GetOrderViewModelByUserIdAsync();
}