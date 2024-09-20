using Bahrin.Harbour.Model.AccountModel;
using PuthaganModel.Admin;

namespace Bahrin.Harbour.Service.AccoutService
{
    public interface IAccountService
    {
        Task<AdminSigninResponse> SignIn(SignInModel signInModel);
        Task CreateUser(AdminUserModel adminUserModel);
        Task<Response> ForgetPassword(ForgetPasswordModel model);
        Task<Response> ResetpasswordOnForgetPassword(ResetPasswordModel model);
    }
}