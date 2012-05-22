using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace zkdao.Email {

    public class EmailServiceByTX : IEmailService {

        public bool SendEmail(string ToAddress, string subject, string content) {
            try {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 25);
                smtpClient.EnableSsl = true; ;

                smtpClient.Credentials = new NetworkCredential("zicjin@qq.com", "rWcsarpi2#qq");

                MailMessage mailMessage = new MailMessage("zicjin@qq.com", ToAddress, subject, content);
                smtpClient.Send(mailMessage);
                return true;
            } catch (Exception ex) {
                ILog Log = LogManager.GetLogger("InfrasEmail", MethodBase.GetCurrentMethod().DeclaringType);
                Log.Warn(ex);
                return false;
            }
        }
    }
}