using Jan_die_alles_kan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Jan_die_alles_kan.Controllers
{
    public class MailController : Controller
    {
        [ValidateInput(false)]
        public ActionResult contactMail()
        {
            string Name = Request.Form["Name"];
            string Message = Request.Form["Message"];
            string Email = Request.Form["Email"];

            SendMailInner("Admin", "Contactmail van " + Name, "Dit schreef " + Name + ":\n" + Message + "\n" + "Mail terug op: " + Email);
            return Redirect("/");
        }
        
        public static void SendMailInner(string username, string subj, string content)
        {
            UserDataContext db = new UserDataContext();

            var EmailAddress = from user in db.DBUserData
                               where user.Username == username
                               select user.Email;

            string email = EmailAddress.ToList().First();
            string subject = subj;
            string emailFrom = "developdejong@gmail.com";
            string password = "hahaHenk82$$";
            MailMessage Mail = new MailMessage(emailFrom, email);
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.Credentials = new NetworkCredential(emailFrom, password);
            client.EnableSsl = true;
            Mail.Subject = subject;
            Mail.To.Add(email);
            Mail.Body = content;
            client.Send(Mail);
        }
    }
}
