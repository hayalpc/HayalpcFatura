using Hayalpc.Fatura.Panel.Internal.Helpers.Interfaces;
using Hayalpc.Library.Common;
using Hayalpc.Library.Common.Dtos;
using Hayalpc.Library.Log;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.Internal.Helpers
{
    public class MailHelper : Hayalpc.Library.Common.Helpers.MailHelper , IMailHelper
    {
        protected readonly Hayalpc.Library.Common.Helpers.Interfaces.IHttpClientHelper clientHelper;

        public MailHelper(IHpLogger logger, Hayalpc.Library.Common.Helpers.Interfaces.IHttpClientHelper clientHelper) : base(logger)
        {
            this.clientHelper = clientHelper;
        }

        public bool SendResetPassword(Common.Dtos.UserDto user, ResetPasswordDto resetPassword)
        {
            var link = Library.Common.Helpers.AppConfigHelper.RealUrl + "create-password/" + resetPassword.Token;

            var msg = $"Sayın {user.FullName},<br><br>" +
                $"Şifrenizi belirlemek için aşağıdaki linki kullanabilirsiniz.<br><br>" +
                $"<a style=\"color:#ffffff; background-color: #ff8300;  border: 10px solid #ff8300; border-radius: 3px; text-decoration:none;\" href=\"{link}\">" +
                $"Devam Et" +
                $"</a><br>" +
                $"<a style='font-size: 10px;color: gray; ' href=\"{link}\">" +
                $"{link}" +
                $"</a><br>";
            //var body = viewRender.RenderToStringAsync("MailTemplates/Template", null).Result;
            var body = clientHelper.Get<string>(Library.Common.Helpers.AppConfigHelper.ApiUrl,"/render/resetPassword");
            body = body.Replace("{{MailContent}}", msg).Replace("{{Title}}", "Şifre Belirleme");
            
            Task.Run(()=> Send(user.Email, "Şifre Belirleme", body)).Forget();

            return true;
        }
        
    }
}
