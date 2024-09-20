using Bahrin.Harbour.Model.EmailModel;

namespace Bahrin.Harbour.Service.EmailService
{
    public interface IEmailService
    {
        Task SendConfirmationEmailOnSignUp(EmailModel.UserMailOptions mailOptions);
        Task  SendEmailForgetPassword(EmailModel.UserMailOptions mailOptions);
        string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs);
    }
}