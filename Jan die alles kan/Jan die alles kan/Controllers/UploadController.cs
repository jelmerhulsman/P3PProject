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

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Upload(UploadModel picture, PictureModel p_model)
        {

            if (picture.File.ContentLength > 0)
            {
                var filename = Path.GetFileName(picture.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Pictures"), filename);
                p_model.File_name = filename;
                picture.File.SaveAs(path);
                db.Picture.Add(p_model);
                db.SaveChanges();
            }
            return RedirectToAction("../Dashboard");
        }
    }
}
