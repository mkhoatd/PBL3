using ECom.Models;
using EComWeb.ViewModels;

namespace EComWeb.Interfaces;

public interface IBasketViewModelService
{
    Task<BasketViewModel> GetOrCreateBasketViewModelForUserAsync(int userId);
    Task<BasketViewModel> MapAsync(Basket basket);
}