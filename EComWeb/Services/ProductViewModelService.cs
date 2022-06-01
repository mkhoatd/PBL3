using ECom.DataAccess.Data;
using ECom.Models;
using EComWeb.Interfaces;
using EComWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

#nullable disable
namespace EComWeb.Services;

public class ProductViewModelService:IProductViewModelService
{
    private readonly ILogger<ProductViewModelService> _logger;
    private readonly ApplicationDbContext _context;

    public ProductViewModelService(
        ILoggerFactory loggerFactory,
        ApplicationDbContext context)
    {
        _logger = loggerFactory.CreateLogger<ProductViewModelService>();
        _context = context;
    }
    public async Task<ProductIndexViewModel> GetProductItemsAsync(int pageIndex, int itemsPage, int? manufactureId, int? categoryId)
    {
        _logger.LogInformation("GetProductItems called.");
        var itemOnPage = await _context.Products.Where(p =>
                (!manufactureId.HasValue || p.ManufactureId == manufactureId) &&
                (!categoryId.HasValue || p.CategoryId == categoryId))
            .Skip(itemsPage * pageIndex).Take(itemsPage).ToListAsync();
        var totalItem = _context.Products
            .Count(p => (!manufactureId.HasValue || p.ManufactureId == manufactureId) &&
                        (!categoryId.HasValue || p.CategoryId == categoryId));
        var vm = new ProductIndexViewModel()
        {
            ProductItems = itemOnPage.Select(p => new ProductItemViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price
            }).ToList(),
            Manufactures = (await GetManufacturesAsync()).ToList(),
            Categories = (await GetCategoryAsync()).ToList(),
            ManufactureFilterApplied = manufactureId ?? 0,
            CategoryFilterApplied = categoryId ?? 0,
            PaginationInfo = new PaginationInfoViewModel()
            {
                ActualPage = pageIndex,
                ItemsPerPage = itemOnPage.Count,
                TotalItems = totalItem,
                TotalPages = int.Parse(Math.Ceiling(((decimal)totalItem/itemsPage)).ToString())
            }
        };
        vm.PaginationInfo.Next=(vm.PaginationInfo.ActualPage==vm.PaginationInfo.TotalPages-1)?"is-disabled":"";
        vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

        return vm;
    }

    public async Task<IEnumerable<SelectListItem>> GetManufacturesAsync()
    {
        _logger.LogInformation("GetManufactuesAsync called.");
        var manufactures = await _context.Manufactures.ToListAsync();
        var items = manufactures
            .Select(m => new SelectListItem() {Value = m.Id.ToString(), Text = m.Name})
            .OrderBy(m => m.Text)
            .ToList();
        var allItem = new SelectListItem() {Value = null, Text = "All", Selected = true};
        items.Insert(0,allItem);
        return items;
    }

    public async Task<IEnumerable<SelectListItem>> GetCategoryAsync()
    {
        _logger.LogInformation("GetManufactuesAsync called.");
        var categories = await _context.Categories.ToListAsync();
        var items = categories
            .Select(m => new SelectListItem() {Value = m.Id.ToString(), Text = m.Name})
            .OrderBy(m => m.Text)
            .ToList();
        var allItem = new SelectListItem() {Value = null, Text = "All", Selected = true};
        items.Insert(0,allItem);
        return items;
    }
}