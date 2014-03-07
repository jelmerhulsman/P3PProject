using Jan_die_alles_kan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jan_die_alles_kan.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View("Dashboard");
        }
        private PagesContext db = new PagesContext();

        public ActionResult PageIndex()
        {
            //PagesModels pagemodel = db.Pages.ToList();
            ViewBag.Content = "content";
            return View(db.Pages.ToList());
        }
        //[Authorize]
        public ActionResult PageCreate()
        {
            return View();
        }
        //[ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PageCreate(PagesModels pagesmodels)
        {
            if (ModelState.IsValid)
            {
                db.Pages.Add(pagesmodels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pagesmodels);
        }

        //[Authorize]
        public ActionResult PageEdit(int id = 0)
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
        public ActionResult PageEdit(PagesModels pagesmodels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagesmodels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pagesmodels);
        }

        //[Authorize]
        public ActionResult PageDelete(int id = 0)
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
        public ActionResult PageDeleteConfirmed(int id)
        {
            PagesModels pagesmodels = db.Pages.Find(id);
            db.Pages.Remove(pagesmodels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //UPLOAD DOWNLOAD

        private UploadDownloadContext uploaddownload = new UploadDownloadContext();
        public ActionResult UploadDownloadIndex()
        {
            return View(uploaddownload.Pictures.ToList());
        }

        public ActionResult UploadDownloadCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadDownloadCreate(UploadDownloadModel uploaddownloadmodel)
        {
            if (ModelState.IsValid)
            {
                uploaddownload.Pictures.Add(uploaddownloadmodel);
                uploaddownload.SaveChanges();
                return RedirectToAction("UploadDownloadIndex");
            }

            return View(uploaddownloadmodel);
        }

        public ActionResult UploadDownloadEdit(int id = 0)
        {
            UploadDownloadModel uploaddownloadmodel = uploaddownload.Pictures.Find(id);
            if (uploaddownloadmodel == null)
            {
                return HttpNotFound();
            }
            return View(uploaddownloadmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadDownloadEdit(UploadDownloadModel uploaddownloadmodel)
        {
            if (ModelState.IsValid)
            {
                uploaddownload.Entry(uploaddownloadmodel).State = EntityState.Modified;
                uploaddownload.SaveChanges();
                return RedirectToAction("UploadDownloadIndex");
            }
            return View(uploaddownloadmodel);
        }

        public ActionResult UploadDownloadDelete(int id = 0)
        {
            UploadDownloadModel uploaddownloadmodel = uploaddownload.Pictures.Find(id);
            if (uploaddownloadmodel == null)
            {
                return HttpNotFound();
            }
            return View(uploaddownloadmodel);
        }

        [HttpPost, ActionName("UploadDownloadDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult UploadDownloadDeleteConfirmed(int id)
        {
            UploadDownloadModel uploaddownloadmodel = uploaddownload.Pictures.Find(id);
            uploaddownload.Pictures.Remove(uploaddownloadmodel);
            uploaddownload.SaveChanges();

            return RedirectToAction("UploadDownloadIndex");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileContentResult UploadDownloaddownload(string name, string route)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(route + name);
            string fileName = name;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }
    }
}
