using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace KW.Application
{
    public interface IEmailService
    {
        void NotifyNewAccount(User user, bool async = false);
        void NotifyResetPassword(string userName, string reqToken, string url);
        void Send(string toEmail, string subject, string bodyMessage, bool async = false);
        void SendGmail(IList<string> emailAddress, string subject, string bodyMessage, byte[] attachment, string attachmentFileName, string fileType, bool async = false);
        void SendGmailFull(IList<string> toAddress, IList<string> ccAddress, IList<string> bccAddress, string subject, string bodyMessage, bool async = false);
        void SendPayrollInformation(string toAddress, string subject, string bodyMessage, bool async = false);
        void SendAutoValidateInformation(string toAddress, string subject, string bodyMessage, bool async = false);
        void SendWithoutAttachment(IList<string> emailAddress, string subject, string bodyMessage, bool async = false);
    }
}
