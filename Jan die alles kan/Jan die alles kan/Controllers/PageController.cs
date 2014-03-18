using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jan_die_alles_kan.Models;

namespace Jan_die_alles_kan.Controllers
{
    public class PageController : Controller
    {

        private PagesContext db = new PagesContext();
        private PicturesContext db_pictures = new PicturesContext();

       
        public ActionResult Landingpage()
        {
            return View();
        }

        public ActionResult Overview()
        {
            return View(db_pictures.Picture.ToList());
        }

        public ActionResult Content(int Id = 0)
        {
            PagesModels pagemodel = db.Pages.Find(Id);

            if (pagemodel == null)
            {
                return HttpNotFound();
            }

            ViewBag.Name = pagemodel.Name;
            ViewBag.Content = pagemodel.Content;

            return View();
        }

        public ActionResult BackToDashboard()
        {
            return RedirectToAction("Dashboard/index");
        }

        public ActionResult Details(int Id = 0)
        {
            PagesModels pagemodel = db.Pages.Find(Id);
            
            if (pagemodel == null)
            {
                return HttpNotFound();
            }

            pagemodel.Content = Shortcodes.ShortSplitter.ShortReplace(pagemodel.Content);

            //ViewBag.Content = pagemodel.Content;
            return View(pagemodel);
             
            //return View("Details");
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}