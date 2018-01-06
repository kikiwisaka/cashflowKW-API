using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KW.Infrastructure.ExternalServices.Email
{
    public class GmailClient
    {
        private static readonly object _synchObj = new object();
        public SmtpClient _smtpClient;
        private string _email = "devonebyone@gmail.com";
        private string _password = "Honestyintegrity123";
        private string _smtpHost = "smtp.gmail.com";
        private int _smtpport = 587;
        private bool _enableSSL = true;

        public GmailClient()
        {
            string email = ConfigurationManager.AppSettings["EmailUserName"];
            if (!string.IsNullOrWhiteSpace(email))
                _email = email;
            string password = ConfigurationManager.AppSettings["EmailPassword"];
            if (!string.IsNullOrWhiteSpace(password))
                _password = password;
            string smtpHost = ConfigurationManager.AppSettings["SMTPHost"];
            if (!string.IsNullOrWhiteSpace(smtpHost))
                _smtpHost = smtpHost;
            string smtpport = ConfigurationManager.AppSettings["SMTPPort"];
            if (!string.IsNullOrWhiteSpace(smtpport))
            {
                try
                {
                    int parsePort = Convert.ToInt32(smtpport);
                    if (parsePort > 0)
                        _smtpport = parsePort;
                }catch(Exception ex){
                    //
                }
            }
            string enableSSL = ConfigurationManager.AppSettings["EnableSSLEmail"];
            if (!string.IsNullOrWhiteSpace(enableSSL))
            {
                try
                {
                    bool isEnable = Convert.ToBoolean(enableSSL);
                    _enableSSL = isEnable;
                }
                catch (Exception ex)
                {
                    //
                }
            }
            _smtpClient = new SmtpClient(_smtpHost);
            _smtpClient.Port = _smtpport;
            _smtpClient.Credentials = new System.Net.NetworkCredential(_email, _password);
            _smtpClient.EnableSsl = _enableSSL;
        }

        public void Send(MailMessage message)
        {
            _smtpClient.Send(message);
        }
        public void SendAsync(MailMessage message)
        {
            _smtpClient.SendAsync(message, null);
        }
    }
}
