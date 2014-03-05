using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jan_die_alles_kan.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            return View("Dashboard");
        }

        [HttpPost]
        public ActionResult PageIndex()
        {
            return RedirectToAction("Index", "PageController");
        }


        //
        // GET: /Dashboard/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

    }
}
