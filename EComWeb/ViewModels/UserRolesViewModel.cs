using System.Collections.Immutable;
using System.Runtime.InteropServices.ComTypes;
using CloudinaryDotNet.Actions;
using ECom.DataAccess.Data;
using ECom.Models;
using ECom.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EComWeb.ViewModels;
public class UserRolesViewMode
{
    private readonly ApplicationDbContext _context;
    public int UserId { get; set; }
    public string Username { get; set; }
    public List<SelectListItem> Roles { get; set; }

    public UserRolesViewMode(int id)
    {
        _context = StaticDetail.ServiceProvider.GetService<ApplicationDbContext>();
        var user = _context.ApplicationUsers.Select(u=>new {u.Id,u.UserName}).FirstOrDefault(u=>u.Id==id);
        Username = user.UserName;
        UserId = id;
        var role = _context.UserRoles.Where(r => r.UserId == UserId).FirstOrDefault();
        Roles=_context.Roles
            .Select(r=>new SelectListItem(){Value = r.Id.ToString(), Text = r.Id.ToString(), Selected = r.Id==role.RoleId?true:false})
            .ToList();
        // viewModel.RoleNames = new List<string>();
        // var user = await _userManager.FindByIdAsync(userId.ToString());
        // viewModel.UserId = userId;
        // viewModel.Username = user.UserName;
        // var roleOfUser = lpublic decimal Price {get; set;}
        // for (int i = 0; i < _roleManager.Roles.Count(); i++)
        // {
        //     viewModel.RoleNames.Add(_roleManager.Roles.ToList()[i].Name);
        //     if (roleOfUser[0] == viewModel.RoleNames[i]) viewModel.SelectedRole = i;
        // }
    }
}