using EComWeb.ViewModels;

namespace EComWeb.Interfaces;

public interface IUserRolesViewModelService
{
    Task<UserRolesViewMode> GetUserRolesViewModelAsync(int userId);
    Task UpdateUserRolesAsync(UserRolesViewMode viewModel);
}