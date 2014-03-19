using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Jan_die_alles_kan.Filters;
using Jan_die_alles_kan.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace Jan_die_alles_kan.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/

        [HttpPost]
        public ActionResult Filter()
        {
            return Json("ABC");
        }

    }
}
