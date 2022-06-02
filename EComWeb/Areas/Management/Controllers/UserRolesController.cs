using ECom.Models;
using EComWeb.Interfaces;
using EComWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EComWeb.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Management")]
    public class UserRolesController : Controller
    {
        private readonly IUserRolesViewModelService _userRolesViewModelService;

        public UserRolesController(IUserRolesViewModelService userRolesViewModelService)
        {
            _userRolesViewModelService = userRolesViewModelService;
        }
        // GET
        public async Task<IActionResult> Index(int userId)
        {
            var viewModel = await _userRolesViewModelService.GetUserRolesViewModelAsync(userId);
            return View(viewModel);
        }
        //POST
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Index(UserRolesViewMode viewModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _userRolesViewModelService.UpdateUserRolesAsync(viewModel);
            return RedirectToAction("Index", "User");
        }
    }
}
