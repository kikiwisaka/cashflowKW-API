using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KW.Common.Helpers
{
    public class EmailHelper
    {
        public delegate string CreateEmailMessage();
        public static SmtpClient EmailConfig()
        {
            SmtpClient client = new SmtpClient();

            if (ConfigHelper.GetValueFromAppSetting("DeliveryEmailMethod") == "2")
            {
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = ConfigHelper.GetValueFromAppSetting("PickupDeliveryFolderPath");
                client.EnableSsl = false;
            }
            else
            {
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = Convert.ToBoolean(ConfigHelper.GetValueFromAppSetting("EnableSMTPSSL"));
                if (!string.IsNullOrEmpty(ConfigHelper.GetValueFromAppSetting("SMTPHostDefault")))
                    client.Host = ConfigHelper.GetValueFromAppSetting("SMTPHostDefault");
                else
                    client.Host = "smtp.mailgun.org";

                if (!string.IsNullOrEmpty(ConfigHelper.GetValueFromAppSetting("SMTPUsernameDefault")))
                    client.Credentials = new NetworkCredential(ConfigHelper.GetValueFromAppSetting("SMTPUsernameDefault"), ConfigHelper.GetValueFromAppSetting("SMTPPasswordDefault"));
                else
                    client.Credentials = new NetworkCredential("postmaster@sandbox8e9efe35ff17414089357dab54b641ee.mailgun.org", "0c71f88a98cfdca192fcd69b134c1935");

                if (!string.IsNullOrEmpty(ConfigHelper.GetValueFromAppSetting("SMTPPortDefault")))
                    client.Port = Convert.ToInt32(ConfigHelper.GetValueFromAppSetting("SMTPPortDefault"));
                else
                    client.Port = 587;
            }
            return client;
        }

        public static MailMessage ConstructContent(string address, string addressName, string subject, string message, CreateEmailMessage CreateEmailMessage = null, string BCCAddress = "")
        {

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(ConfigHelper.GetValueFromAppSetting("SiteEmailDefault"), ConfigHelper.GetValueFromAppSetting("SiteEmailNameDefault"));

            mailMessage.To.Add(new MailAddress(address, string.IsNullOrEmpty(addressName) ? address : addressName));

            if (BCCAddress != "" && BCCAddress.Contains(','))
            {
                string[] bcc = BCCAddress.Split(',');
                foreach (string bc in bcc)
                {
                    if (!string.IsNullOrEmpty(bc))
                        mailMessage.Bcc.Add(new MailAddress(bc));
                }
            }
            else if (BCCAddress != "")
                mailMessage.Bcc.Add(new MailAddress(BCCAddress));

            mailMessage.Subject = subject;
            string msgStr = message;
            if (msgStr == "" && CreateEmailMessage != null)
            {
                msgStr = CreateEmailMessage();
                message = msgStr;
            }
            mailMessage.Body = message;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;

            return mailMessage;
        }
    }
}
