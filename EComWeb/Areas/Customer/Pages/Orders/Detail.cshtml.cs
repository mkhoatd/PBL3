using ECom.Models;
using EComWeb.Interfaces;
using EComWeb.Services;
using EComWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EComWeb.Areas.Customer.Pages.Orders;
[Authorize(Roles="Admin,User")]
public class DetailModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOrderViewModelService _orderViewModelService;
    private readonly ILogger<DetailModel> _logger;

    public DetailModel(UserManager<ApplicationUser> userManager, IOrderViewModelService orderViewModelService,
        ILogger<DetailModel> logger)
    {
        _userManager = userManager;
        _orderViewModelService = orderViewModelService;
        _logger = logger;
    }
    public OrderViewModel Order { get; set; }
    public async Task<IActionResult> OnGetAsync(int orderId)
    {
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        Order = await _orderViewModelService.GetOrderDetailAsync(orderId);
        if (currentUser.Id != Order.BuyerId) return Forbid();
        return Page();
    }
}