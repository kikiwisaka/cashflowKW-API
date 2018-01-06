using KW.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.Templates.Email
{
    public class RegisterNewUser
    {
        public MailMessage Generate(User user)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("noreply@onebyonedigital.com", "OneByOne");
            mail.To.Add(user.UserName);
            mail.Subject = string.Format("New Employee registered using this email address by {0}", user.UserName);
            mail.Body = bodyMessage(user.UserName, user.Password);
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            return mail;
        }

        private string bodyMessage(string userName, string password)
        {
            #region Template
            var template = @"<table align='center' border='0' cellpadding='0' cellspacing='0' width='750' style='border: 1px solid #cccccc; border-collapse: collapse;'>
                                <tbody>
                                    <tr>
                                        <td align='left' style='padding: 0px 0 1px 0; color: #153643; font-size: 28px; font-weight: bold; font-family: Arial, sans-serif; box-shadow: 0 5px 4px -7px #707F8C;'>
                                            <img  src='data:image/gif;base64,{4}' style='margin-left: 220px; margin-top: 10px; position: relative;'>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor='#ffffff' style='padding: 20px 30px 40px 30px;'>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                <tbody>
                                                    <tr>
                                                        <td style='color: #563d7c; font-family: Arial, sans-serif; font-size: 25px;'>
                                                            <b>Welcome to Advansys</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style='padding: 20px 0 30px 0; color: #7C7C7C; font-family: Arial, sans-serif; font-size: 13px; line-height: 20px;'>
                                                            You have been registered {1} in Advansys HR<br/><br/>
                                                            To accept this registration, please login to <a href='https://KW.com/login'>https://KW.com/login</a> with following login :
                                                            Username : {2}<br/>
                                                            Temporary password : {2}<br/><br/>
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
            #endregion
            return string.Format(template, userName, password);
        }
    }
}
