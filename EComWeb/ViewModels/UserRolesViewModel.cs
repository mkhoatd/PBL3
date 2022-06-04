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

#nullable disable

namespace EComWeb.ViewModels;
public class UserRolesViewMode
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public List<SelectListItem> Roles { get; set; }
    public string CurrentRole { get; set; }
}