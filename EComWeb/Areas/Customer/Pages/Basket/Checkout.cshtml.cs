using ECom.Models;
using EComWeb.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EComWeb.Areas.Customer.Pages.Basket;

public class CheckoutModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IBasketViewModelService _basketViewModelService;
    private readonly IBasketService _basketService;
    
    public void OnGet()
    {
        
    }
}