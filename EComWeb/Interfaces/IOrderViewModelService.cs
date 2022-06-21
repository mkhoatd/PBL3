using EComWeb.ViewModels;

namespace EComWeb.Interfaces;

public interface IOrderViewModelService
{
    Task<List<OrderViewModel>> GetAllOrderAsync(int userId);
    Task<OrderViewModel> GetOrderDetailAsync(int orderId);
}