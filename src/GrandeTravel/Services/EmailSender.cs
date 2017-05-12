using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.Services
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string from, string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from, from));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("grandetravelproject@gmail.com", "Diplomaproject");
                client.Send(message);
                client.Disconnect(true);
            }
            
        }
    }
}
