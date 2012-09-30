using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace NiCris.CoreServices.Interfaces
{
    public interface IEmailService
    {
        // Sync
        bool SendEmail(MailMessage mail);
        bool SendEmail(string to, string subject, string body);
        bool SendEmail(string from, string to, string subject, string body);

        // Async
        void SendEmailAsync(MailMessage mail);
        void SendEmailAsync(string subject, string body, bool isBodyHtml);
        void SendEmailAsync(string to, string subject, string body);
    }
}