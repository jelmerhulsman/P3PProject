using Jan_die_alles_kan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jan_die_alles_kan.Controllers
{
    public class UploadController : Controller
    {
        private PicturesContext db = new PicturesContext();

        public ActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadImage(UploadModel picture, PictureModel p_model)
        {

            if (picture.File.ContentLength > 0)
            {
                var filename = Path.GetFileName(picture.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Pictures"), filename);
                p_model.File_name = filename;
                p_model.CTime = DateTime.Now;
                p_model.MTime = DateTime.Now;
                picture.File.SaveAs(path);
                db.Picture.Add(p_model);
                db.SaveChanges();
            }
            return RedirectToAction("../Dashboard");
        }



        public ActionResult EditImage()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditImage(UploadModel picture, PictureModel p_model)
        {

            if (picture.File.ContentLength > 0)
            {
                var filename = Path.GetFileName(picture.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Pictures"), filename);
                p_model.File_name = filename;
                p_model.CTime = DateTime.Now;
                p_model.MTime = DateTime.Now;
                picture.File.SaveAs(path);
                db.Picture.Add(p_model);
                db.SaveChanges();
            }
            return RedirectToAction("../Dashboard");
        }


        public ActionResult IndexImage()
        {
            return View(db.Picture.ToList());
        }
    }
}
