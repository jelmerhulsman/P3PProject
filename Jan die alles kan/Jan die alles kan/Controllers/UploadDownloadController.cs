using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jan_die_alles_kan.Models;

namespace Jan_die_alles_kan.Controllers
{
    public class UploadDownloadController : Controller
    {
        private UploadDownloadContext db = new UploadDownloadContext();

        public ActionResult Index()
        {
            return View(db.Pictures.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            UploadDownloadModel uploaddownloadmodel = db.Pictures.Find(id);
            if (uploaddownloadmodel == null)
            {
                return HttpNotFound();
            }
            return View(uploaddownloadmodel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UploadDownloadModel uploaddownloadmodel)
        {
            if (ModelState.IsValid)
            {
                db.Pictures.Add(uploaddownloadmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uploaddownloadmodel);
        }

        public ActionResult Edit(int id = 0)
        {
            UploadDownloadModel uploaddownloadmodel = db.Pictures.Find(id);
            if (uploaddownloadmodel == null)
            {
                return HttpNotFound();
            }
            return View(uploaddownloadmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UploadDownloadModel uploaddownloadmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uploaddownloadmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uploaddownloadmodel);
        }

        public ActionResult Delete(int id = 0)
        {
            UploadDownloadModel uploaddownloadmodel = db.Pictures.Find(id);
            if (uploaddownloadmodel == null)
            {
                return HttpNotFound();
            }
            return View(uploaddownloadmodel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UploadDownloadModel uploaddownloadmodel = db.Pictures.Find(id);
            db.Pictures.Remove(uploaddownloadmodel);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileContentResult download(string name, string route)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(route + name);
            string fileName = name;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            
        }

    }
}