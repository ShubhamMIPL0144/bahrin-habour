using Bahrin.Harbour.Model.AppUserAuth;
using Bahrin.Harbour.Service.AppUserService;
using Microsoft.AspNetCore.Mvc;

namespace Bahrin_Harbour.Areas.Administration.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IAppUserService _appUserService;

        public UserManagementController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        public async Task<IActionResult> AppUsers()
        {
            var users = await _appUserService.GetAllAppUsersAsync();
            return View(users);
        }
        public async Task<IActionResult> GetAppUserById(string id)
        {
            var appUser = await _appUserService.GetAppUserByEmailAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return Ok(appUser);
        }

        public IActionResult CreateAppUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUser(AppUserViewModel appUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.AddAppUserAsync(appUserViewModel);
                if (result.status)
                {
                    TempData["Success"] = "User Created SuccessFully. Password Sent on Email";
                    return RedirectToAction("Administration/UserManagement/AppUsers");
                }
                TempData["Error"] = result.message;
            }
            return View(appUserViewModel);
        }
        public async Task<IActionResult> UpdateAppUser(string id)
        {
            var appUser = await _appUserService.GetAppUserByEmailAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAppUser(AppUserViewModel appUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.UpdateAppUserAsync(appUserViewModel);
                if (result.status)
                {
                    return RedirectToAction("Administration/UserManagement/AppUsers");
                }
                ModelState.AddModelError("", result.message);
            }
            return View(appUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAppUser(string id)
        {
            var result = await _appUserService.HardDeleteAppUserAsync(id);
            if (result.status)
            {
                return RedirectToAction("Administration/UserManagement/AppUsers");
            }
            return NotFound(result.message);
        }

        [HttpPost]
        public async Task<IActionResult> DeActivateUser(string id)
        {
            var result = await _appUserService.DeActivateUser(id);
            if (result.status)
            {
                return RedirectToAction("Administration/UserManagement/AppUsers");
            }
            return NotFound(result.message);
        }
    }
}
