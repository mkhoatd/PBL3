using EComWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EComWeb.Interfaces;

public interface IProductViewModelService
{
    Task<ProductIndexViewModel> GetProductItemsAsync(int pageIndex, int itemsPage, string? name, int? manufactureId, int? categoryId);
    Task<IEnumerable<SelectListItem>> GetManufacturesAsync();
    Task<IEnumerable<SelectListItem>> GetCategoryAsync();
}