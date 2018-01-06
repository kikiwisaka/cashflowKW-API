using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.Templates.Email
{
    public class ResetPassword
    {
        public MailMessage Generate(string userName, string requestToken, string url)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("dev@onebyonedigital.com", "AdvansSys");
            mail.To.Add(userName);
            mail.Subject = string.Format("Reset password for this {0} account", userName);
            mail.Body = bodyMessage(userName, url, requestToken);
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            return mail;
        }

        public string bodyMessage(string userName, string url, string reqToken)
        {
            #region Template
            var template = @"<table align='center' border='0' cellpadding='0' cellspacing='0' width='750' style='border: 1px solid #cccccc; border-collapse: collapse;'>
                                <tbody>
                                    <tr>
                                        <td align='left' style='padding: 0px 0 1px 0; color: #184315; font-size: 28px; font-weight: bold; font-family: Arial, sans-serif; box-shadow: 0 5px 4px -7px #707F8C;'>
                                            <img  src='data:image/gif;base64' style='margin-left: 220px; margin-top: 10px; position: relative;'>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor='#ffffff' style='padding: 20px 30px 40px 30px;'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                <tbody>
                                                    <tr>
                                                        <td style='color: #60903c; font-family: Arial, sans-serif; font-size: 25px;'>
                                                            <b>AdvanSys</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style='padding: 20px 0 30px 0; color: #373637; font-family: Arial, sans-serif; font-size: 13px; line-height: 20px;'>
                                                            Hi, {0} <br/><br/>
                                                            To Reset Your Password, simply follow this link <a href='{1}/Home/ChangeConfirm?token={2}'>{1}/Home/ChangeConfirm?token={2}</a><br/><br/>                                                           
                                                            * note :<br/>
                                                            Your account will automatically disable if not activate within 7 days
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='padding:10px 140px 10px 30px;font-family: Trebuchet MS,Verdana,Arial,Helvetica,sans-serif; font-size: 10px; color:#333;'>
                                            This email is sent automatically by the system. Do not reply to e-mail because we do not monitor this inbox.
                                        </td>
                                    </tr>
                                </tbody>
                            </table>";
            #endregion template
            /**
            http://localhost:52263/Home/ChangeConfirm?token={1}
            **/
            return string.Format(template, userName, url, reqToken);
        }
    }
}
