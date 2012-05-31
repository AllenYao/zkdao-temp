using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Net.Mime;

namespace zkdao.Email {

    public class EmailServiceByTX : IEmailService {
        public void SendEmail(string ToAddress, string subject, string content, bool isRich) {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true; //QQ不支持
            smtpClient.Credentials = new NetworkCredential("zicjin@gmail.com", "rWcsarpi2#gm");

            //If need support outlook http://goo.gl/Wbjiv
            //MailMessage mailMessage = new MailMessage("zicjin@gmail.com", ToAddress, subject, content);
            //mailMessage.IsBodyHtml = false;
            //AlternateView altView = AlternateView.CreateAlternateViewFromString(richContent, new ContentType(MediaTypeNames.Text.Html));
            //mailMessage.AlternateViews.Add(altView);

            MailMessage mailMessage = new MailMessage("zicjin@gmail.com", ToAddress, subject, content);
            mailMessage.IsBodyHtml = isRich;

            try {
                smtpClient.Send(mailMessage);
            } catch (Exception ex) {
                ILog Log = LogManager.GetLogger("InfrasEmail", MethodBase.GetCurrentMethod().DeclaringType);
                Log.Warn(ex);
            }
            //altView.Dispose();
        }
    }
}