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
    public class CustSecurityController : Controller
    {
        private CustSecurityContext db = new CustSecurityContext();

        public ActionResult Index()
        {
            return View(db.IPProfiles.ToList());
        }

        public IPProfile[] Details()
        {
            IPProfile[] ipprofile = db.IPProfiles.ToArray<IPProfile>();
            if (ipprofile == null)
            {
                return null;
            }
            return ipprofile;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IPProfile ipprofile)
        {
            if (ModelState.IsValid)
            {
                db.IPProfiles.Add(ipprofile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ipprofile);
        }

        public ActionResult Edit(int id = 0)
        {
            IPProfile ipprofile = db.IPProfiles.Find(id);
            if (ipprofile == null)
            {
                return HttpNotFound();
            }
            return View(ipprofile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IPProfile ipprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ipprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ipprofile);
        }

        public ActionResult Delete(int id = 0)
        {
            IPProfile ipprofile = db.IPProfiles.Find(id);
            if (ipprofile == null)
            {
                return HttpNotFound();
            }
            return View(ipprofile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IPProfile ipprofile = db.IPProfiles.Find(id);
            db.IPProfiles.Remove(ipprofile);
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