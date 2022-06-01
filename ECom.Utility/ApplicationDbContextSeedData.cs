using ECom.DataAccess.Data;
using ECom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ECom.Utility;

public class ApplicationDbContextSeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetService<ApplicationDbContext>();
        var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
        string[] roles = new string[] {StaticDetail.RoleUser, StaticDetail.RoleAdmin};
        foreach (var role in roles)
        {
            if(!context.Roles.Any(x => x.Name == role))
            {
                context.Roles.Add(new ApplicationRole()
                {
                    Name = role,
                    NormalizedName = role.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });
            }

            context.SaveChanges();
        }

        if (!Queryable.Any(context.UserRoles, x => 
                x.RoleId == Queryable.FirstOrDefault(context.Roles, r => r.Name == StaticDetail.RoleAdmin).Id))
        {
            
            var user= new ApplicationUser
            {
                UserName = StaticDetail.AdminEmail,
                Email=StaticDetail.AdminEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, StaticDetail.AdminPassword);
            await userManager.AddToRoleAsync(user, StaticDetail.RoleAdmin);
            context.SaveChanges();
        }

        if (!context.Manufactures.Any())
        {
            var lst = new List<Manufacture>()
            {
                new Manufacture
                {
                    Name = "Apple"
                },
                new Manufacture
                {
                    Name = "Samsung"
                },
                new Manufacture
                {
                    Name = "Sony"
                },
                new Manufacture
                {
                    Name = "OnePlus"
                }
            };
            context.Manufactures.AddRange(lst);
            context.SaveChanges();
        }

        if (!context.Categories.Any())
        {
            var lst = new List<Category>()
            {
                new Category
                {
                    Name = "Điện thoại"
                },
                new Category
                {
                    Name = "Máy tính bảng"
                },
                new Category
                {
                    Name = "Laptop"
                }
            };
            context.Categories.AddRange(lst);
            context.SaveChanges();
        }
    }
}