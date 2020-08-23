using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DishUp.Controllers
{
    public class MailManager
    {


        public void EnviarMail(string asunto, string body, string to)
        {
            // Plug in your email service here to send an email.
            SmtpClient smtpCliente = new SmtpClient();
            smtpCliente.Host = "smtp.gmail.com";
            smtpCliente.Port = 587;
            smtpCliente.EnableSsl = true;
            smtpCliente.UseDefaultCredentials = false;
            smtpCliente.Credentials = new NetworkCredential { UserName = "federicobatista@gmail.com", Password = "G4l4xys4!!" };
            body = body.Replace("#EMAIL#", to);
            var mailMEssage = new MailMessage("federicobatista@gmail.com", to);
            mailMEssage.Subject = asunto;
            mailMEssage.Body = body;
            mailMEssage.IsBodyHtml = true;
            smtpCliente.Send(mailMEssage);
        }
    }
}