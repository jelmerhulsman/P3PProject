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


        public ActionResult Details(int id = 0)
        {
            UploadDownloadModel uploaddownloadmodel = db.Pictures.Find(id);
            if (uploaddownloadmodel == null)
            {
                return HttpNotFound();
            }
            return View(uploaddownloadmodel);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



    }
}