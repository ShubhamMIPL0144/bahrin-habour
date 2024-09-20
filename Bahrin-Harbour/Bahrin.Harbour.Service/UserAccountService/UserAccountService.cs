using Bahrin.Harbour.Helper;
using Bahrin.Harbour.Model.AccountModel;
using Bahrin.Harbour.Model.AppUserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bahrin.Harbour.Service.UserAccountService
{
    public class UserAccountService : IUserAccountService
    {
      /*  public SigninResponse Signin(SignInModel data)
        {
            SigninResponse response = new SigninResponse();
            try
            {
                User user = iLoginDA.Signin(data);
                if (user != null)
                {
                    if (!user.isActive && user._id != new Guid())
                    {
                        response.status = new StatusModel { status = EnumHelper.status.Success.ToString(), message = Constants.AccountDisable };
                    }
                    else if (user._id != new Guid())
                    {
                        response.data = new UserModel
                        {
                            _id = user._id,
                            userName = user.userName,
                            name = user.name,
                            dateofBirth = user.dateofBirth,
                            email = user.email,
                            phoneNumber = user.phoneNumber,
                            image = user.image,
                            deviceToken = user.deviceToken,
                            createdDate = user.createdDate,
                            modifiedDate = user.modifiedDate,
                            createdBy = user.createdBy,
                            modifiedBy = user.modifiedBy,
                            isActive = user.isActive,
                            isDeleted = user.isDeleted,
                            token = user.token,

                        };
                        response.data.image = iAws3Service.GetFileFullPath(response.data.image, iAdminDA.GetJsonFileContent<ApplicationSettingModel>(Constants.ApplicationSetting).userImageUrl, Constants.DefaultUserImage);

                        string subscriptionPlanId = iSubscriptionService.GetUserSubscriptionPlans(response.data._id)
                        .Where(x => x.status.ToLower() == EnumHelper.SubscriptionStatus.active.ToString()).Select(x => x.id)
                        .FirstOrDefault();

                        response.status = new StatusModel { status = EnumHelper.status.Success.ToString(), message = Constants.successfullyLogin };
                    }
                    else
                    {
                        response.status = new StatusModel { status = EnumHelper.status.Fail.ToString(), message = Constants.DataNotFound };
                    }
                }
                else
                {
                    response.status = new StatusModel { status = EnumHelper.status.Fail.ToString(), message = Constants.WrongCredential };
                }
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }*/
    }
}
