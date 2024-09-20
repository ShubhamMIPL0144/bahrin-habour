using Bahrin.Harbour.Helper;
using Bahrin.Harbour.Model.AccountModel;
using Bahrin.Harbour.Model.AppUserAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bahrin_Harbour.Areas.AppUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 

        /* [HttpPost]
        [Route("Signin")]
        [ProducesResponseType(typeof(SigninResponse), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(SigninResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       public ActionResult<SigninResponse> Signin(SignInModel data)
        {
            try
            {
                SigninResponse response = iLoginService.Signin(data);
                if (response.data != null)
                {
                  //response.data.token = iHelper.GenerateAuthenticationToken(response.data.name, response.data.userName, response.data._id.ToString(), response.data.token);
                    iLoginService.UpdateUserAuthenticationToken(response.data._id, response.data.token);
                    return Accepted(response);
                }
                else
                {
                    return NotFound(response);
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }*/
    }
}
