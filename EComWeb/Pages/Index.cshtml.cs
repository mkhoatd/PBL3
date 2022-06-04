using EComWeb.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EComWeb.ViewModels;

namespace EComWeb.Pages;

public class IndexModel : PageModel
{
    private readonly IProductViewModelService _productViewModelService;

    public IndexModel(IProductViewModelService productViewModelService)
    {
        _productViewModelService = productViewModelService;
    }

    public ProductIndexViewModel ProductIndexViewModel { get; set; } = new ProductIndexViewModel();

    public async Task OnGetAsync(ProductIndexViewModel productIndexViewModel, int? pageId)
    {
        ProductIndexViewModel = await _productViewModelService.GetProductItemsAsync(pageId ?? 0,
            Constants.ITEMS_PER_PAGE, productIndexViewModel.NameFilterApplied, productIndexViewModel.ManufactureFilterApplied,
            productIndexViewModel.CategoryFilterApplied);
    }
}