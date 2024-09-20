using Bahrin.Harbour.Data.DBCollections;
using Bahrin.Harbour.Helper;
using Bahrin.Harbour.Model.AccountModel;
using Bahrin.Harbour.Model.ProjectSession;
using Bahrin.Harbour.Service.AccoutService;
using Bahrin.Harbour.Service.EmailService;
using Bahrin_Harbour.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static Bahrin.Harbour.Model.EmailModel.EmailModel;

namespace Bahrin_Harbour.Areas.Administration.Controllers
{
   // [AutoValidateAntiforgeryToken]
    [Area("Administration")]
    [Route("[area]/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService iAccountService;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _email;
        private readonly SignInManager<ApplicationUser> iSignInManager;
        private readonly UserManager<ApplicationUser> iUserManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IAccountService AccountService, ILogger<HomeController> logger, IAccountService accountService, IConfiguration configuration, IEmailService email)
        {
            iSignInManager = signInManager;
            iUserManager = userManager;
            iAccountService = AccountService;
            _logger = logger;
            _configuration = configuration;
            _email = email;
        }

        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                signInModel.isUpdate = true;

                if (string.IsNullOrWhiteSpace(signInModel.email) || string.IsNullOrWhiteSpace(signInModel.password))
                {
                    return View(signInModel);
                }

                var result = await iAccountService.SignIn(signInModel);

                if (result.status.status)
                {
                    HttpContext.Session.SetString("UserEmail", result.data.email);
                    HttpContext.Session.SetString("UserId", result.data._id.ToString());
                    HttpContext.Session.SetString("UserRoles", string.Join(",", result.status.roles));
                    ProjectSessionModel.admin = result.data;
                   // ProjectSessionModel.dateFormat = iSettingService.GetdateFormateSetttingModel();
                    ProjectSessionModel.localTimeZoneOffset = signInModel.localTimeZoneOffset;

                    if (result.status.roles.Contains("SuperAdmin"))
                    {
                        return RedirectToAction("Dashboard", "Dashboard");
                    }
                }
                else
                {
                    ModelState.AddModelError("", result.status.message);
                }
            }

            return View(signInModel);
        }

        [AdminAuthorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            HttpContext.Session.Clear();
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            ProjectSessionModel.admin = null;

            return RedirectToAction("Signin", "Account");
        }
         
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel passwordModel)
        {
            if (ModelState.IsValid)
            {
                var ResetMailSent = await iAccountService.ForgetPassword(passwordModel);
                if (ResetMailSent.Success)
                {
                    ViewBag.Success = "Reset Link Sent on your Mail Successfully";
                }
                else
                {
                    ViewBag.Error = ResetMailSent.Message;
                }
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string uid, string token)
        {
            ResetPasswordModel model = new ResetPasswordModel()
            {
                UserId = uid,
                Token = token
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetmodel)
        {
            if (ModelState.IsValid)
            {
                var result = await iAccountService.ResetpasswordOnForgetPassword(resetmodel);

                if (result.Success)
                {
                    ViewBag.Success = result.Message;
                    return View("SuccessfulPasswordrest"); 
                }
                else
                {
                    ViewBag.Error = result.Message;
                    return View(resetmodel); 
                }
            }
            return View(resetmodel);
        }

        /// <summary>
        /// ////Create Admin Users by SuperAdmin
        /// </summary>
        /// <returns></returns>
        /// 
        [AdminAuthorize]
        public IActionResult CreateAdminUser()
        {
            return View();
        }

        [AdminAuthorize]
        [HttpPost]
        public async Task<IActionResult> CreateAdminUser(AdminUserModel adminUserModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await iAccountService.CreateUser(adminUserModel);

            return Ok();
        }
    }
}
