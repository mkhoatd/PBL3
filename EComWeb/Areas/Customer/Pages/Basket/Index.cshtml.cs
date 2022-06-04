﻿using ECom.DataAccess.Data;
using ECom.Models;
using EComWeb.Interfaces;
using EComWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EComWeb.Areas.Customer.Pages.Basket;

[Authorize(Roles="Admin,User")]
public class IndexModel : PageModel
{
    private readonly IBasketService _basketService;
    private readonly IBasketViewModelService _basketViewModelService;
    private readonly UserManager<ApplicationUser> _userManager;
    private ApplicationDbContext _context;

    public IndexModel(IBasketService basketService,
        IBasketViewModelService basketViewModelService,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context)
    {
        _basketService = basketService;
        _basketViewModelService = basketViewModelService;
        _userManager = userManager;
        _context = context;
    }

    public BasketViewModel BasketViewModel { get; set; } = new BasketViewModel();

    private async Task<int> GetUserIdAsync()
    {
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        return currentUser.Id;
    }
    
    public async Task OnGetAsync()
    {
        BasketViewModel = await _basketViewModelService.GetOrCreateBasketViewModelForUserAsync(await GetUserIdAsync());
    }

    public async Task<IActionResult> OnPostAsync(ProductItemViewModel productDetails)
    {
        if (productDetails?.Id == null)
        {
            return RedirectToPage("/Index");
        }

        var item = await _context.Products.FindAsync(productDetails.Id);
        if (item == null)
        {
            return RedirectToPage("/Index");
        }

        var userId = await GetUserIdAsync();
        var basket = await _basketService.AddItemToBasketAsync(userId, productDetails.Id, 1);
        BasketViewModel = await _basketViewModelService.MapAsync(basket);

        return RedirectToPage();
    }

    public async Task OnPostUpdate(IEnumerable<BasketItemViewModel> items)
    {
        if (!ModelState.IsValid)
        {
            return;
        }

        var basketView = await _basketViewModelService.GetOrCreateBasketViewModelForUserAsync(await GetUserIdAsync());
        var updateModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);
        var basket = await _basketService.SetQuantitesAsync(basketView.Id, updateModel);
        BasketViewModel = await _basketViewModelService.MapAsync(basket);
    }
}