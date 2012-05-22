using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zkdao.Email {

    public interface IEmailService {

        bool SendEmail(string ToAddress, string subject, string content);
    }
}