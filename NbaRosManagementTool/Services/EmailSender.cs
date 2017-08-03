using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace NbaRosManagementTool.Services
{
    public class EmailSender : IEmailSender 
    {
        public static string Validate { get; set; }

        public Task SendEmailAsync(string subject, string message,string email)
        {
            //Smtp server 
            string SmtpServer = "smtp.live.com";
            

            //Smtp Port Number 
            int SmtpPortNumber = 587;

            
         
            string FromAdressTitle = "NBA Free Agent Offer";
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, email));
            mimeMessage.To.Add(new MailboxAddress(email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = message

            };

           

            using (var client = new SmtpClient())
            {
                client.Connect(SmtpServer, SmtpPortNumber, false);
                // Note: only needed if the SMTP server requires authentication 
                // Error 5.5.1 Authentication 
                client.Authenticate(email,Validate);
                client.Send(mimeMessage);
                client.Disconnect(true);
                return Task.FromResult(0);
            }

        }

        
    }
}
