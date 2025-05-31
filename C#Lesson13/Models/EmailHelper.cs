using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmailhelperNamespace
{
    public static class EmailHelper
    {
        public static void SendEmailToAdmin(string subject, string body)
        {
            var fromAddress = new MailAddress("nrmin.mrdova.01@bk.ru", "App Notification");
            var toAddress = new MailAddress("narminmurshudova@outlook.com", "Admin");
            const string fromPassword = "lWocXqoQsO9kOSOhEmj9";

            var smtp = new SmtpClient
            {
                Host = "smtp.mail.ru",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };


            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }

}
