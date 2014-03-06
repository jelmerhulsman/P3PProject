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

        //[Authorize]
        public ActionResult Index()
        {
            //PagesModels pagemodel = db.Pages.ToList();
            ViewBag.Content = "content";
            return View(db.Pages.ToList());
        }

        public ActionResult Landingpage()
        {
            return View();
        }

        public ActionResult Overview()
        {
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

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        //[ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PagesModels pagesmodels)
        {
            if (ModelState.IsValid)
            {
                db.Pages.Add(pagesmodels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pagesmodels);
        }

        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            PagesModels pagesmodels = db.Pages.Find(id);
            if (pagesmodels == null)
            {
                return HttpNotFound();
            }
            return View(pagesmodels);
        }
       
        //[ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PagesModels pagesmodels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagesmodels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pagesmodels);
        }

        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            PagesModels pagesmodels = db.Pages.Find(id);
            if (pagesmodels == null)
            {
                return HttpNotFound();
            }
            return View(pagesmodels);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PagesModels pagesmodels = db.Pages.Find(id);
            db.Pages.Remove(pagesmodels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}