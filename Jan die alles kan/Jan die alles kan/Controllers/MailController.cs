﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace Jan_die_alles_kan.Controllers
{
    public class MailController : Controller
    {
       
           
        
        [HttpPost]
        public ActionResult SendMail()
        {
            string email = Request.Form["email"];
            string subject = Request.Form["subject"];
            string emailFrom = "developdejong@gmail.com";
            string password = "darktranquillity";
            MailMessage Mail = new MailMessage(emailFrom, email);
            SmtpClient client = new SmtpClient();
            client.Port= 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.Credentials = new NetworkCredential(emailFrom, password);
            client.EnableSsl = true;
            Mail.Subject = subject;
            Mail.To.Add(email);
            Mail.Body = "this is my test email body";
            client.Send(Mail);
             return Redirect("/page/index");
        }
    }
}
