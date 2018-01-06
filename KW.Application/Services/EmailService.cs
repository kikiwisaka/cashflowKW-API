using KW.Core;
using KW.Domain;
using System;
using System.Collections.Generic;
using KW.Application.DTO;
using KW.Application.Templates.Email;
using KW.Infrastructure.ExternalServices.Email;
using System.Net.Mail;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.IO;
using KW.Common.Extensions;

namespace KW.Application
{
    public class EmailService : IEmailService
    {
        private string senderEmail;
        private string senderName;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public EmailService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;

            NameValueCollection appConfig = ConfigurationManager.AppSettings;
            senderEmail = appConfig["EmailSender"];
            senderName = appConfig["EmailSenderName"];
        }

        public void NotifyNewAccount(User user, bool async = false)
        {


            var mailMessage = new RegisterNewUser().Generate(user);
            new GmailClient().Send(mailMessage);
        }

        public void NotifyResetPassword(string userName, string reqToken, string url)
        {
            var mailMessage = new ResetPassword().Generate(userName, reqToken, url);
            new GmailClient().Send(mailMessage);
        }

        public void Send(string toEmail, string subject, string bodyMessage, bool async = false)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail, senderName);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = bodyMessage;
            mailMessage.BodyEncoding = UTF8Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            new GmailClient().Send(mailMessage);
        }

        public void Send(string fromEmail, string fromName, string toEmail, string subject, string bodyMessage, bool async = false)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromEmail, fromName);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = bodyMessage;
            mailMessage.BodyEncoding = UTF8Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            new GmailClient().Send(mailMessage);
        }
        public void SendPayrollInformation(string toAddress, string subject, string bodyMessage, bool async = false)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail, senderName);
                mailMessage.To.Add(toAddress);
                mailMessage.Subject = subject;
                mailMessage.Body = bodyMessage;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                new GmailClient().Send(mailMessage);
            }
            catch (Exception x)
            {
                string errror = x.Message;
            }
        }

        public void SendAutoValidateInformation(string toAddress, string subject, string bodyMessage, bool async = false)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail, senderName);
                mailMessage.To.Add(toAddress);
                mailMessage.Subject = subject;
                mailMessage.Body = bodyMessage;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                new GmailClient().Send(mailMessage);
            }
            catch (Exception x)
            {
                string errror = x.Message;
            }
        }

        public void SendGmail(IList<string> toAddress, string subject, string bodyMessage, byte[] attachmentFile, string attachmentFileName, string fileType, bool async = false)
        {
            try
            {
                using (Stream fileStream = new MemoryStream(attachmentFile))
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(senderEmail, senderName);
                    mailMessage.Subject = subject;
                    mailMessage.Body = bodyMessage;
                    mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    foreach (var address in toAddress)
                    {
                        mailMessage.To.Add(address);
                        mailMessage.To.Add(address);
                    }

                    if (attachmentFile.IsNotNull())
                    {
                        Attachment attachment = new Attachment(fileStream, attachmentFileName, fileType); //"application/pdf"
                        mailMessage.Attachments.Add(attachment);
                    };

                    new GmailClient().Send(mailMessage);
                }
            }
            catch (Exception x)
            {
                string errror = x.Message;
            }

        }

        public void SendWithoutAttachment(IList<string> toAddress, string subject, string bodyMessage, bool async = false)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail, senderName);
                mailMessage.Subject = subject;
                mailMessage.Body = bodyMessage;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                foreach (var address in toAddress)
                {
                    mailMessage.To.Add(address);
                }

                new GmailClient().Send(mailMessage);
            }
            catch (Exception x)
            {
                string errror = x.Message;
            }

        }

        public void SendGmailFull(IList<string> toAddress, IList<string> ccAddress, IList<string> bccAddress, string subject, string bodyMessage, bool async)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail, senderName);
                mailMessage.Subject = subject;
                mailMessage.Body = bodyMessage;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                foreach (var address in toAddress)
                    mailMessage.To.Add(address);

                if (ccAddress.Count > 0)
                {
                    foreach (var address in ccAddress)
                        mailMessage.CC.Add(address);
                }

                if (bccAddress.Count > 0)
                {
                    foreach (var address in bccAddress)
                        mailMessage.Bcc.Add(address);
                }

                if (async)
                {
                    new GmailClient().Send(mailMessage);
                }
                else
                {
                    new GmailClient().Send(mailMessage);
                }
            }
            catch (Exception x)
            {
                string errror = x.Message;
            }

        }
    }
}
