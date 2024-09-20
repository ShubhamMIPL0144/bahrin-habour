using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;
using static Bahrin.Harbour.Model.EmailModel.EmailModel;

namespace Bahrin.Harbour.Service.EmailService
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"wwwroot/Email/Templates/{0}.html";
        private readonly SMTPConfigModel _configmodel;

        public EmailService(IOptions<SMTPConfigModel> configmodel)
        {
            _configmodel = configmodel.Value;
        }

        public async Task SendEmailForgetPassword(UserMailOptions mailOptions)
        {
            mailOptions.Subject = UpdatePlaceHolders("{{UserName}} Password Reset Request", mailOptions.PlaceHolders);
            mailOptions.Body = UpdatePlaceHolders(FindTemplate("ForgetPasswordSendMail"), mailOptions.PlaceHolders);

            await SendEmail(mailOptions);
        }

        public async Task SendConfirmationEmailOnSignUp(UserMailOptions mailOptions)
        {
            mailOptions.Subject = UpdatePlaceHolders("{{FirstName}} {{LastName}} Account Created Successfully", mailOptions.PlaceHolders);
            mailOptions.Body = UpdatePlaceHolders(FindTemplate("ConfirmEmailOnSignUp"), mailOptions.PlaceHolders);

            await SendEmail(mailOptions);
        }
        private async Task SendEmail(UserMailOptions userMailOptions)
        {
            MailMessage mailMessage = new MailMessage()
            {
                Subject = userMailOptions.Subject,
                Body = userMailOptions.Body,
                IsBodyHtml = _configmodel.IsHTMLBody,
                From = new MailAddress(_configmodel.SenderAddress, _configmodel.SenderDisplayName)
            };

            foreach (var mailAddress in userMailOptions.ToEmail)
            {
                mailMessage.To.Add(mailAddress);
            }

            NetworkCredential networkCredential = new NetworkCredential()
            {
                UserName = _configmodel.UserName,
                Password = _configmodel.Password
            };

            SmtpClient smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io", 25)
            {
                EnableSsl = _configmodel.EnableSSL,
                UseDefaultCredentials = _configmodel.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mailMessage.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mailMessage);
        }

        private string FindTemplate(string templateName)
        {
            var template = File.ReadAllText(string.Format(templatePath, templateName));
            return template;
        }
        public string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var pair in keyValuePairs)
                {
                    if (text.Contains(pair.Key))
                    {
                        text = text.Replace(pair.Key, pair.Value);
                    }
                }
                return text;
            }
            return text;
        }
    }
}
