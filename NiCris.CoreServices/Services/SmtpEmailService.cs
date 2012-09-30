using System;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using NiCris.CoreServices.Interfaces;
using NiCris.CoreServices.Utilities;
using NLog;


namespace NiCris.CoreServices.Services
{
    public class SmtpEmailService : IEmailService
    {
        static Logger _logger = LogManager.GetCurrentClassLogger();

        // *** Sync
        public bool SendEmail(MailMessage mail)
        {
            bool result = false;
            try
            {
                SmtpClient client = new SmtpClient();

                NetworkCredential credentials = new NetworkCredential();
                client.Credentials = credentials;

                client.Send(mail);
                result = true;
            }
            catch (Exception ex)
            {
                _logger.ErrorException("*** An Exception occurred while trying to send an email\n", ex);
            }
            finally
            {
                if (mail != null) mail.Dispose();
            }
            return result;
        }

        public bool SendEmail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            return SendEmail(mail);
        }

        public bool SendEmail(string from, string to, string subject, string body)
        {
            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;
            return SendEmail(message);            
        }

        // *** Async
        public void SendEmailAsync(MailMessage mail)
        {
            try
            {
                // Get out of the ASP.NET context to speed up rendering...
                using (new SynchronizationContextSwitcher())
                {
                    SmtpClient client = new SmtpClient();
                    NetworkCredential credentials = new NetworkCredential();
                    client.Credentials = credentials;
                    client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

                    object userState = mail;
                    client.SendAsync(mail, userState);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("*** An Exception occurred while trying to send an email (async)\n", ex);
            }
        }

        public void SendEmailAsync(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            SendEmailAsync(mail);
        }

        public void SendEmailAsync(string subject, string body, bool isBodyHtml)
        {
            string recipientsSetting = ConfigurationManager.AppSettings["EmailRecipients"];

            if (string.IsNullOrEmpty(recipientsSetting))
                throw new Exception("The AppSetting EmailRecipients is not supplied.");

            string[] recipients = recipientsSetting.Split(';');
            
            MailMessage mail = new MailMessage();
            
            foreach (var recipient in recipients)
                mail.To.Add(recipient);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;
            SendEmailAsync(mail);
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the Original MailMessage object
            MailMessage mail = (MailMessage)e.UserState;

            if (e.Cancelled)
                _logger.Warn("Send canceled for mail with subject [{0}].", mail.Subject);
            if (e.Error != null)
                _logger.Error("Error {0} occurred when sending mail [{1}] ", e.Error.ToString(), mail.Subject);
            else
                _logger.Info("Email message [{0}] sent.", mail.Subject);

            // Clean up
            if (mail != null) mail.Dispose();
        }
    }
}
