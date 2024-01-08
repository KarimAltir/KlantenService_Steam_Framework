using KlantenService_Steam_Framework.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using KlantenService_Steam_Framework.Areas.Identity.Data;
using Microsoft.Extensions.Options;
using NETCore.MailKit.Infrastructure.Internal;
using static KlantenService_Steam_Framework.Services.MailService;

namespace KlantenService_Steam_Framework
{
    static public class Globals
    {
        static public KlantenServiceUser DummyUser { get; set; }

        static public Dictionary<string, Parameter> Parameters { get; set; }

        static public WebApplication App { get; set; }

        static public void ConfigureMail()
        {
            MailKitEmailSender mailSender = (MailKitEmailSender)App.Services.GetService<IEmailSender>();
            var options = mailSender.Options;
            options.Server = Parameters["Mail.Server"].Value;
            options.Server = Parameters["Mail.Server"].Value;
            options.Port = Convert.ToInt32(Parameters["Mail.Port"].Value);
            options.Account = Parameters["Mail.Account"].Value;
            options.Password = Parameters["Mail.Password"].Value;
            options.SenderEmail = Parameters["Mail.SenderEmail"].Value;
            options.SenderName = Parameters["Mail.SenderName"].Value;
            options.Security = Convert.ToBoolean(Parameters["Mail.Security"].Value);

        }   
    }
}
