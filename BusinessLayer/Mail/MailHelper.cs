using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text;
namespace BusinessLayer.Mail
{
    public class MailHelper
    {
        public static void SendMail(List<string> to, string from, string subject, string body)
        {
            MailMessage mailMsg = new MailMessage();

            foreach (var emailAddress in to)
                mailMsg.To.Add(emailAddress);
            mailMsg.From = new MailAddress(from);

            mailMsg.Subject = subject;
            mailMsg.Body = body.Replace("\n", "<br/>");
            mailMsg.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("CiprianV", "elearning1");
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
        }


    }
}





