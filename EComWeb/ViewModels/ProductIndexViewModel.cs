using Microsoft.AspNetCore.Mvc.Rendering;

namespace EComWeb.ViewModels;

public class ProductIndexViewModel
{
    public List<ProductItemViewModel> ProductItems { get; set; }
    public List<SelectListItem> Manufactures { get; set; }
    public List<SelectListItem> Categories { get; set; }
    public int? ManufactureFilterApplied { get; set; }
    public int? CategoryFilterApplied { get; set; }
    public PaginationInfoViewModel PaginationInfo { get; set; }
}