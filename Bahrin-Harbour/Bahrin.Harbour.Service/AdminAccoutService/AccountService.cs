using Bahrin.Harbour.Data.DBCollections;
using Bahrin.Harbour.Model.AccountModel;
using Bahrin.Harbour.Service.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text;
using static Bahrin.Harbour.Model.EmailModel.EmailModel;

namespace Bahrin.Harbour.Service.AccoutService
{
    public class AccountService : IAccountService
    {

        private readonly SignInManager<ApplicationUser> iSignInManager;
        private readonly UserManager<ApplicationUser> iUserManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _email;

        public AccountService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailService email)
        {
            iSignInManager = signInManager;
            iUserManager = userManager;
            _configuration = configuration;
            _email = email;
        }

        public async Task<AdminSigninResponse> SignIn(SignInModel signInModel)
        {
            var adminResponse = new AdminSigninResponse();

            if (signInModel == null || string.IsNullOrWhiteSpace(signInModel.email) || string.IsNullOrWhiteSpace(signInModel.password))
            {
                adminResponse.status = new StatusModel
                {
                    status = false,
                    message = "Invalid input data"
                };
                return adminResponse;
            }

            try
            {
                var adminModel = await iUserManager.FindByEmailAsync(signInModel.email);

                if (adminModel != null)
                {
                    var result = await iSignInManager.PasswordSignInAsync(adminModel.UserName, signInModel.password, signInModel.rememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var roles = await iUserManager.GetRolesAsync(adminModel);

                        adminResponse.data = new Model.AccountModel.AdminModel
                        {
                            email = adminModel.Email,
                            _id = Guid.Parse(adminModel.Id),
                        };

                        adminResponse.status = new StatusModel
                        {

                            status = true,
                            message = "Login successfully",
                            roles = roles.ToList()
                        };
                    }
                    else
                    {
                        adminResponse.status = new StatusModel
                        {
                            status = false,
                            message = "Invalid credentials"
                        };
                    }
                }
                else
                {
                    adminResponse.status = new StatusModel
                    {
                        status = false,
                        message = "Details not found"
                    };
                }
            }
            catch (Exception ex)
            {
                adminResponse.status = new StatusModel
                {
                    status = false,
                    message = "An error occurred during login"
                };
            }

            return adminResponse;
        }


        public async Task<Response> ForgetPassword(ForgetPasswordModel model)
        {
            var user = iUserManager.FindByEmailAsync(model.email).Result;
            if (user != null)
            {
                var passwordResetToken = await iUserManager.GeneratePasswordResetTokenAsync(user);
                if (!string.IsNullOrEmpty(passwordResetToken))
                {
                    passwordResetToken = passwordResetToken + "###" + DateTimeOffset.Now.AddMinutes(3).ToUnixTimeSeconds().ToString();
                 //   var btokenBase64string = Convert.ToBase64String(Encoding.UTF8.GetBytes(passwordResetToken));
                    var btokenBase64string = Helper.Helper.EnryptString(passwordResetToken);
                    var result = await SendMailOnForgetPassword(model, user, btokenBase64string);

                    return new Response { Success = result, Message = "Successful" };
                }
                return new Response { Success = false, Message = "Password Reset failed" }; ;
            }
            return new Response { Success = false, Message = "User does not exist" };
        }
        public async Task<bool> SendMailOnForgetPassword(ForgetPasswordModel model, ApplicationUser user, String token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string ConfirmationEmail = _configuration.GetSection("Application:ForgetPassword").Value;

            UserMailOptions mailOptions = new UserMailOptions()
            {
                ToEmail = new List<string>() { model.email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName ),
                    new KeyValuePair<string, string>("{{ResetLink}}", string.Format(appDomain + ConfirmationEmail, model.email, token) )
                }
            };
            try
            {
                await _email.SendEmailForgetPassword(mailOptions);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> ResetpasswordOnForgetPassword(ResetPasswordModel model)
        {
            //  string decodedtoken = Encoding.UTF8.GetString(Convert.FromBase64String(model.Token));
            string decodedtoken = Helper.Helper.DecryptString(model.Token);

            string[] splitString = decodedtoken.Split("###");
            if (Convert.ToInt32(splitString[1]) > DateTimeOffset.Now.ToUnixTimeSeconds())
            {

                model.Token = splitString[0];
                var user = iUserManager.FindByEmailAsync(model.UserId).Result;
                if (user != null)
                {
                    var passwordResetToken = await iUserManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    if (passwordResetToken.Succeeded)
                    {
                        return new Response { Success = true, Message = "Successful" };
                    }
                    return new Response { Success = false, Message = string.Join(", ", passwordResetToken.Errors.Select(x => x.Description).ToList()) }; ;
                }
                return new Response { Success = false, Message = "User does not exist" };
            }
            return new Response { Success = false, Message = "Password reset link expire" };
        }

        public async Task CreateUser(AdminUserModel adminUserModel)
        {
            var user = new ApplicationUser()
            {
                Email = adminUserModel.Email,
                PhoneNumber = adminUserModel.PhoneNumber,
                PhoneNumberConfirmed = adminUserModel.PhoneNumberConfirmed,
                FirstName = adminUserModel.FirstName,
                LastName = adminUserModel.LastName,
                Address = adminUserModel.Address,
                State = adminUserModel.State,
                Country = adminUserModel.Country,
                IsActive = adminUserModel.IsActive,
                CreatedBy = null,////////////////use user id of signed in user
                CreatedOn = DateTime.Now,
            };

            var existingUser = await iUserManager.FindByEmailAsync(user.Email);
            if (existingUser == null)
            {
                var password = GeneratePassword();
                var result = await iUserManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    var res = await iUserManager.AddToRoleAsync(user, "Admin");
                    if (res.Succeeded)
                    {
                        ///send Email and password on email address
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating user {user.Email}: {error.Description}");
                    }
                }
            }

        }

        private static string GeneratePassword(int length = 8)
        {
            if (length < 8) throw new ArgumentException("Password must be at least 8 characters.");

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }
    }


}
