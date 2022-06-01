using ECom.Models;
using EComWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EComWeb.Controllers;

[Authorize(Roles = "Admin")]
public class UserRolesController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public UserRolesController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }
    // GET
    public async Task<IActionResult> Index(int userId)
    {
        var viewModel = new UserRolesViewMode(userId);
        return View(viewModel);
    }
    //POST
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Index(UserRolesViewMode viewMode)
    {
        if (!ModelState.IsValid) return BadRequest();
        var user = await _userManager.FindByIdAsync(viewMode.UserId.ToString());
        for (int i = 0; i < viewMode.Roles.Count; i++)
        {
            if (viewMode.Roles[i].Selected == true) await _userManager.AddToRoleAsync(user, viewMode.Roles[i].Text);
            else await _userManager.RemoveFromRoleAsync(user, viewMode.Roles[i].Text);
        }
        return RedirectToAction("Index","User");
    }
}