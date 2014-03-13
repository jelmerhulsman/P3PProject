using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jan_die_alles_kan.Models;
using System.IO;

namespace Jan_die_alles_kan.Controllers
{
    public class CatogeryController : Controller
    {
        private CatogeryContext db = new CatogeryContext();

        //
        // GET: /Catogery/

        public ActionResult Index()
        {
            return View(db.Catogeries.ToList());
        }

        //
        // GET: /Catogery/Details/5

        public ActionResult Details(int id = 0)
        {
            Catogery catogery = db.Catogeries.Find(id);
            if (catogery == null)
            {
                return HttpNotFound();
            }
            return View(catogery);
        }

        //
        // GET: /Catogery/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Catogery/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Catogery catogery)
        {
            if (ModelState.IsValid)
            {
                db.Catogeries.Add(catogery);
                db.SaveChanges();
                string pad = Server.MapPath("~/Images/Catogeries/" + catogery.Name);
                Directory.CreateDirectory(pad);
                return RedirectToAction("Index");
            }

            return View(catogery);
        }

        //
        // GET: /Catogery/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Catogery catogery = db.Catogeries.Find(id);
            if (catogery == null)
            {
                return HttpNotFound();
            }
            return View(catogery);
        }

        //
        // POST: /Catogery/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Catogery catogery)
        {
            if (ModelState.IsValid)
            {
                Catogery previous = db.Catogeries.Find(catogery.Id);
                db.Catogeries.Remove(previous);
                db.Catogeries.Add(catogery);
                db.SaveChanges();
                
                string pad = Server.MapPath("~/Images/Catogeries/" + catogery.Name);
                string oldPad = Server.MapPath("~/Images/Catogeries/" + previous.Name);
                Directory.Move(oldPad, pad);

                return RedirectToAction("Index");
            }
            return View(catogery);
        }

        //
        // GET: /Catogery/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Catogery catogery = db.Catogeries.Find(id);
            if (catogery == null)
            {
                return HttpNotFound();
            }
            return View(catogery);
        }

        //
        // POST: /Catogery/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catogery catogery = db.Catogeries.Find(id);
            string pad = Server.MapPath("~/Images/Catogeries/" + catogery.Name);

            Directory.Delete(pad);
            db.Catogeries.Remove(catogery);
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