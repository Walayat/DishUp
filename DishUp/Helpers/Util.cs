using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DishUp.Helpers
{
    public class Util
    {
        public void SendEmail(Dictionary<int,int?> dictionary)
        {
            var fromAddress = new MailAddress("", "");
            var toAddress = new MailAddress("", "");
            var subject = "New Catering Order";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "")

            };

            var msg = new MailMessage
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = "",

            };
            smtp.Send(msg);
            
        }
    }
}