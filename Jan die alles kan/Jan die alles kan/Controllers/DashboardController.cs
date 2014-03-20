using Jan_die_alles_kan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Jan_die_alles_kan.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SendMail()
        {
            return View("SendMail");
        }

        [ValidateInput(false)]
        public ActionResult SendMail2()
        {
            string email = Request.Form["email"];
            string subject = Request.Form["subject"];
            string content = Request.Unvalidated.Form["content"];
            string emailFrom = "developdejong@gmail.com";
            string password = "darktranquillity";
            MailMessage Mail = new MailMessage(emailFrom, email);
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.Credentials = new NetworkCredential(emailFrom, password);
            client.EnableSsl = true;
            Mail.Subject = subject;
            Mail.To.Add(email);
            //WebUtility.HtmlEncode(content);
            Mail.Body = content;
            Mail.IsBodyHtml = true;
            client.Send(Mail);
            return Redirect("../Dashboard/Index");
        }
#region pages LOGIC
        private PagesContext db = new PagesContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Upload()
        {
            return View("Upload");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult PageIndex()
        {
            //PagesModels pagemodel = db.Pages.ToList();
            ViewBag.Content = "content";
            return View(db.Pages.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult PageCreate()
        {
            return View();
        }
        //[ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize (Roles="Admin")]
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult PageDeleteConfirmed(int id)
        {
            PagesModels pagesmodels = db.Pages.Find(id);
            db.Pages.Remove(pagesmodels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
#endregion
        
        private PicturesContext db2 = new PicturesContext();
        private CategoryContext dbcategories = new CategoryContext();
#region Image LOGIC
        [Authorize(Roles = "Admin")]
        public ActionResult ImageUpload()
        {

            Category c = new Category();
            return View(dbcategories.Categories.ToList());

        }

        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult ImageUpload(UploadModel picture, PictureModel p_model, Category c_model)
        {

            if (picture.File.ContentLength > 0)
            {
                var filename = Path.GetFileName(picture.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/Categories/" + p_model.Category + "/"), filename);
                string thumppad = Server.MapPath("~/Images/Categories/" + p_model.Category + "/Thumbnails/");
                Directory.CreateDirectory(thumppad);
                var ThumPath = Path.Combine(Server.MapPath("~/Images/Categories/" + p_model.Category + "/Thumbnails/"), filename);

                p_model.File_name = filename;
                p_model.CTime = DateTime.Now;
                p_model.MTime = DateTime.Now;
                picture.File.SaveAs(path);
                Image Thumb = makeThumb(picture.File);
                Thumb.Save(ThumPath);
                db2.Picture.Add(p_model);
                db2.SaveChanges();
            }
            return RedirectToAction("../Dashboard/Index");
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            
            
            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        private Image makeThumb(HttpPostedFileBase file)
        {
            Image inputimage = Image.FromStream(file.InputStream, true, true);
            Image logo = Image.FromFile(Server.MapPath("~/Images/milanovLogo.png"));
            Image image = ScaleImage(inputimage, 300, 300);
            Graphics g = System.Drawing.Graphics.FromImage(image);

            
            Bitmap TransparentLogo = new Bitmap(image.Width, image.Height);
            
            
            Graphics TGraphics = Graphics.FromImage(TransparentLogo);
            ColorMatrix ColorMatrix = new ColorMatrix();
            ColorMatrix.Matrix33 = 0.50F; //transparantie watermerk
            ImageAttributes ImgAttributes = new ImageAttributes();
            ImgAttributes.SetColorMatrix(ColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            TGraphics.DrawImage(logo, new Rectangle(0, 0, TransparentLogo.Width, TransparentLogo.Height), 0, 0, 300, 300, GraphicsUnit.Pixel, ImgAttributes);
            TGraphics.Dispose();
            g.DrawImage(TransparentLogo, (image.Width/8) , (image.Height / 4));

            return image;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ImageEdit(int id = 0)
        {
            PictureModel Picturemodel = db2.Picture.Find(id);
            var categorieContext = new CategoryContext();
            var query = categorieContext.Categories.Where(c => c.Name == Picturemodel.Category);
            if (Picturemodel == null)
            {
                return HttpNotFound();
            }

            ViewData["Photo"] = Picturemodel;
            ViewData["Categorie"] = query;

            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult ImageEdit(PictureModel p_model)
        {
            ModelState.Remove("CTime");
            if (ModelState.IsValid)
            {
                p_model.MTime = DateTime.Now;
                //p_model.CTime = p_model.CTime;
                //db2.Entry(p_model).Entity.CTime;
                db2.Entry(p_model).State = EntityState.Modified;
                //db2.Picture.Add(p_model);
                db2.SaveChanges();
                return RedirectToAction("../Dashboard/Index");
            }
            return View(p_model);
        
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ImageIndex()
        {
            return View(db2.Picture.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ImageDelete(int id = 0)
        {
            PictureModel Picturemodel = db2.Picture.Find(id);
            if (Picturemodel == null)
            {
                return HttpNotFound();
            }
            return View(Picturemodel);
        }
        [HttpPost, ActionName("ImageDelete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult ImageDeleteConfirmed(int id)
        {
            PictureModel Picturemodel = db2.Picture.Find(id);
            string pad = Server.MapPath("~/Images/Categories/" + Picturemodel.Category + "/" + Picturemodel.File_name);
            string Thumbpad = Server.MapPath("~/Images/Categories/" + Picturemodel.Category + "/Thumbnails/" + Picturemodel.File_name);
            System.IO.File.Delete(pad);
            try
            {
                System.IO.File.Delete(Thumbpad);
            }
            catch { }
            db2.Picture.Remove(Picturemodel);
            db2.SaveChanges();
            return RedirectToAction("Index");
        }
#endregion
        //private CategoryContext db = new CategoryContext();

        //
        // GET: /Category/

        [Authorize(Roles = "Admin")]
        public ActionResult CategoryIndex()
        {
            return View(dbcategories.Categories.ToList());
        }


        //
        // GET: /Category/CategoryCreate

        [Authorize(Roles = "Admin")]
        public ActionResult CategoryCreate()
        {
            return View();
        }

        //
        // POST: /Category/CategoryCreate

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryCreate(Category category)
        {
            if (ModelState.IsValid)
            {
                var intermediate = from cato in dbcategories.Categories
                                   where cato.Name == category.Name
                                   select cato;
                if (intermediate.Count() == 0)
                {
                    dbcategories.Categories.Add(category);
                    dbcategories.SaveChanges();
                    string pad = Server.MapPath("~/Images/Categories/" + category.Name);
                    Directory.CreateDirectory(pad);
                }

                return RedirectToAction("/CategoryIndex");

            }

            return View(category);
        }

        ////
        //// GET: /Category/CategoryEdit/5

        //public ActionResult CategoryEdit(int id = 0)
        //{
        //    Category category = dbcategories.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        ////
        //// POST: /Category/CategoryEdit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CategoryEdit(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Category previous = dbcategories.Categories.Find(category.ID);
        //        dbcategories.Categories.Remove(previous);
        //        dbcategories.Categories.Add(category);
        //        dbcategories.SaveChanges();

        //        string pad = Server.MapPath("~/Images/Categories/" + category.Name);
        //        string oldPad = Server.MapPath("~/Images/Categories/" + previous.Name);
        //        Directory.Move(oldPad, pad);

        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        //
        // GET: /Category/CategoryDelete/5

        [Authorize(Roles = "Admin")]
        public ActionResult CategoryDelete(int id = 0)
        {
            Category category = dbcategories.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/CategoryDelete/5

        [HttpPost, ActionName("CategoryDelete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryDeleteConfirmed(int id)
        {
            Category category = dbcategories.Categories.Find(id);
            string pad = Server.MapPath("~/Images/Categories/" + category.Name);
            try
            {
                Directory.Delete(pad);
            }
            catch
            {
            }
            dbcategories.Categories.Remove(category);
            dbcategories.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
