using ActionMailer.Net.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jan_die_alles_kan.Controllers
{
    public class MailController : MailerBase
    {

        public EmailResult Sample()
        {

                From = "no-reply@mysite.nl";
                To.Add("sanderd18@gmail.com");
                Subject = "testtest mail test succes";
                return Email("EmailView");

        }

    }
}
