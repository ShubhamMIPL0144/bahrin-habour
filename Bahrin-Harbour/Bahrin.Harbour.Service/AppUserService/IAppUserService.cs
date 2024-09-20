using Bahrin.Harbour.Data.DBCollections;
using Bahrin.Harbour.Model.AccountModel;
using Bahrin.Harbour.Model.AppUserAuth;

namespace Bahrin.Harbour.Service.AppUserService
{
    public interface IAppUserService
    {
        Task<StatusModel> AddAppUserAsync(AppUserViewModel appUser);
        Task<StatusModel> DeActivateUser(string userId);
        Task<List<AppUserViewModel>> GetAllAppUsersAsync();
        Task<AppUserViewModel> GetAppUserByEmailAsync(string email);
        Task<StatusModel> HardDeleteAppUserAsync(string userId);
        Task<bool> SendMailOnAccountCreation(ApplicationUser user, string Password);
        Task<StatusModel> UpdateAppUserAsync(AppUserViewModel appUser);
    }
}