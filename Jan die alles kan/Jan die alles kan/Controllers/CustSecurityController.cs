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

        //
        // GET: /CustSecurity/

        public ActionResult Index()
        {
            return View(db.IPProfiles.ToList());
        }

        //
        // GET: /CustSecurity/Details/5

        public IPProfile[] Details()
        {
            IPProfile[] ipprofile = db.IPProfiles.ToArray<IPProfile>();
            if (ipprofile == null)
            {
                return null;
            }
            return ipprofile;
        }

        //
        // GET: /CustSecurity/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CustSecurity/Create

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

        //
        // GET: /CustSecurity/Edit/5

        public ActionResult Edit(int id = 0)
        {
            IPProfile ipprofile = db.IPProfiles.Find(id);
            if (ipprofile == null)
            {
                return HttpNotFound();
            }
            return View(ipprofile);
        }

        //
        // POST: /CustSecurity/Edit/5

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

        //
        // GET: /CustSecurity/Delete/5

        public ActionResult Delete(int id = 0)
        {
            IPProfile ipprofile = db.IPProfiles.Find(id);
            if (ipprofile == null)
            {
                return HttpNotFound();
            }
            return View(ipprofile);
        }

        //
        // POST: /CustSecurity/Delete/5

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