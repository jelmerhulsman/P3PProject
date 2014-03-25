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

    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult SendMail()
        {
            UserDataContext db = new UserDataContext();
            var username = from user in db.DBUserData select user.Username;
            ViewBag.username = username;
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
            email = getEmail(email);
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

        private string getEmail(string userName)
        {
            string Email = "";
            UserDataContext db = new UserDataContext();
            var mail = from user in db.DBUserData
                       where user.Username == userName
                       select user.Email;
            try
            {
                string adress = mail.ToList().First();
                var addr = new System.Net.Mail.MailAddress(adress);
                Email = adress;
            }
            catch
            {
                Email = "developdejong@gmail.com";
            }

            return Email;
        }
        #region pages LOGIC

        private PagesContext db = new PagesContext();

        public ActionResult PageIndex()
        {
            ViewBag.Content = "content";
            return View(db.Pages.ToList());
        }

        public ActionResult PageCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PageCreate(PagesModels pagesmodels)
        {
            if (ModelState.IsValid)
            {
                string edit = pagesmodels.Permalink;

                edit = char.ToUpper(edit[0]) + edit.Substring(1).ToLower();
                edit = edit.Trim();
                edit.Replace(" ", string.Empty);

                pagesmodels.Permalink = edit;

                if (pagesmodels.Content == null)
                    pagesmodels.Content = " ";

                db.Pages.Add(pagesmodels);
                db.SaveChanges();
                return RedirectToAction("PageIndex");
            }

            return View(pagesmodels);
        }

        public ActionResult PageEdit(int id = 0)
        {
            PagesModels pagesmodels = db.Pages.Find(id);
            if (pagesmodels == null)
            {
                return HttpNotFound();
            }
            return View(pagesmodels);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PageEdit(PagesModels pagesmodels)
        {
            string edit = pagesmodels.Permalink;

            edit = char.ToUpper(edit[0]) + edit.Substring(1).ToLower();
            edit = edit.Trim();
            edit.Replace(" ", string.Empty);

            pagesmodels.Permalink = edit;
            if (ModelState.IsValid)
            {
                db.Entry(pagesmodels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PageIndex");
            }
            return View(pagesmodels);
        }

        public ActionResult PageShow(string Id = "")
        {
            return RedirectToAction("../Page/Content/" + Id);
        }

        public ActionResult PageDelete(int id = 0)
        {
            PagesModels pagesmodels = db.Pages.Find(id);
            if (pagesmodels == null)
            {
                return HttpNotFound();
            }
            return View(pagesmodels);
        }

        [HttpPost, ActionName("PageDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PageDeleteConfirmed(int id)
        {
            PagesModels pagesmodels = db.Pages.Find(id);
            db.Pages.Remove(pagesmodels);
            db.SaveChanges();
            return RedirectToAction("PageIndex");
        }
        #endregion

        private PicturesContext db2 = new PicturesContext();

        private CategoryContext dbcategories = new CategoryContext();

        #region Image LOGIC

        //SPecial method to save jpg, this is to prevent errors
        public static void SaveJpeg
(string path, Image img)
        {
            System.IO.MemoryStream mss = new System.IO.MemoryStream();

            System.IO.FileStream fs
            = new System.IO.FileStream(path, System.IO.FileMode.Create
            , System.IO.FileAccess.ReadWrite);

            img.Save(mss, ImageFormat.Jpeg);
            byte[] matriz = mss.ToArray();
            fs.Write(matriz, 0, matriz.Length);

            mss.Close();
            fs.Close();
        }

        public ActionResult ImageUpload()
        {

            Category c = new Category();
            return View(dbcategories.Categories.ToList());

        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ImageUpload(UploadModel picture, PictureModel p_model, Category c_model)
        {
            string fileExt = Path.GetExtension(picture.File.FileName);
            var filename = Path.GetFileName(picture.File.FileName);
            bool DatabaseFilename = (from name in db2.Picture
                                   where name.File_name == filename
                                   select name).Any();
            
            if (picture.File.ContentLength > 0 && (fileExt == "jpeg." || fileExt == ".jpg") && DatabaseFilename == false)
            {
                //THUMBNAIL GENERATOR               
                string thumbpad = Server.MapPath("~/Images/Categories/" + p_model.Category + "/Thumbnails/" + picture.File.FileName);
                Directory.CreateDirectory(Server.MapPath("~/Images/Categories/" + p_model.Category + "/Thumbnails/"));
                Image Thumb = makeThumb(picture.File, true);
                SaveJpeg(thumbpad, Thumb);

                //PREVIEW GENERATOR
                var PreviewPath = Server.MapPath("~/Images/Categories/" + p_model.Category + "/Previews/" + picture.File.FileName);
                Directory.CreateDirectory(Server.MapPath("~/Images/Categories/" + p_model.Category + "/Previews/"));
                Image Preview = makeThumb(picture.File, false);
                SaveJpeg(PreviewPath, Preview);
                

                p_model.File_name = filename;
                p_model.CTime = DateTime.Now;
                p_model.MTime = DateTime.Now;

                if (Preview.Width >= Preview.Width)
                    p_model.Orientation = "horizontal";
                else
                    p_model.Orientation = "vertical";

                //Actual image upload
                Image image = Image.FromStream(picture.File.InputStream,true,true);
                var path = Server.MapPath("~/Images/Categories/" + p_model.Category + "/" + filename);
                SaveJpeg(path, image);
                
                db2.Picture.Add(p_model);
                db2.SaveChanges(); 
            }
            else
            {
                if (DatabaseFilename == true)
                    return Json("The filename " + filename + " is already used. Please provide an other filename.");
                return Json("There was an error processing your file, please only upload JPG and JPEG images.");
            }
            return RedirectToAction("ImageIndex");
        }

        //Method to scale an image, with a boolean for scaling
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight, bool bRatio)
        {

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);


            var newImage = new Bitmap(newWidth, newHeight);
            if (bRatio)
                Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            else
            {
                newImage = new Bitmap(maxWidth, maxHeight);
                Graphics.FromImage(newImage).DrawImage(image, 0, 0, maxWidth, maxHeight);
            }
            return newImage;
        }

        //Method for making a thumbnail or a preview image. This method also puts in a watermark
        private Image makeThumb(HttpPostedFileBase file, bool bIsThumb)
        {
            Image inputimage = Image.FromStream(file.InputStream, true, true);
            Image logo = Image.FromFile(Server.MapPath("~/Images/milanovLogo.png"));
            Image image = inputimage;

            

            Graphics g = System.Drawing.Graphics.FromImage(image);


            Bitmap TransparentLogo = new Bitmap(image.Width, image.Height); //gebied waar het logo word geplaatst


            Graphics TGraphics = Graphics.FromImage(TransparentLogo);
            ColorMatrix ColorMatrix = new ColorMatrix();
            ColorMatrix.Matrix33 = 0.50F; //transparantie watermerk
            ImageAttributes ImgAttributes = new ImageAttributes();
            ImgAttributes.SetColorMatrix(ColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            TGraphics.DrawImage(logo, new Rectangle(0, 0, TransparentLogo.Width, TransparentLogo.Height), 0, 0, logo.Width, logo.Height, GraphicsUnit.Pixel, ImgAttributes);
            TGraphics.Dispose();
            g.DrawImage(TransparentLogo, 0/*(image.Width / 2) - (logo.Width /2 )*/, 0/*(image.Height / 2) - (logo.Height /2)*/);
            if (bIsThumb)
            {
                image = ScaleImage(inputimage, 300, 300, true);
                logo = ScaleImage(logo, 300, 300, true);
            }
            else
                logo = ScaleImage(logo, image.Width, image.Height, true);
            return image;
        }

        public ActionResult ImageEdit(int id = 0)
        {
            PictureModel Picturemodel = db2.Picture.Find(id);

            if (Picturemodel == null)
            {
                return HttpNotFound();
            }

            return View(Picturemodel);
        }


        [HttpPost]
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
                return RedirectToAction("ImageIndex");
            }
            return View(p_model);

        }
        
        public ActionResult ImageIndex()
        {
            return View(db2.Picture.ToList());
        }

        public ActionResult ImageDelete(int id = 0)
        {
            PictureModel picturemodel = db2.Picture.Find(id);
            if (picturemodel == null)
            {
                return HttpNotFound();
            }
            return View(picturemodel);
        }

        [HttpPost, ActionName("ImageDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ImageDeleteConfirmed(int id)
        {
            PictureModel Picturemodel = db2.Picture.Find(id);
            string pad = Server.MapPath("~/Images/Categories/" + Picturemodel.Category + "/" + Picturemodel.File_name);
            string Thumbpad = Server.MapPath("~/Images/Categories/" + Picturemodel.Category + "/Thumbnails/" + Picturemodel.File_name);
            string Prevpad = Server.MapPath("~/Images/Categories/" + Picturemodel.Category + "/Previews/" + Picturemodel.File_name);
            try
            {
                System.IO.File.Delete(Prevpad);
                System.IO.File.Delete(pad);
                System.IO.File.Delete(Thumbpad);
            }
            catch
            {
                return Json("The system was unable to delete the image/thumbnail/preview");
            }
            db2.Picture.Remove(Picturemodel);
            db2.SaveChanges();
            return RedirectToAction("ImageIndex");
        }
        #endregion

        //
        // GET: /Category/

        public ActionResult CategoryIndex()
        {
            return View(dbcategories.Categories.ToList());
        }


        //
        // GET: /Category/CategoryCreate

        public ActionResult CategoryCreate()
        {
            return View();
        }

        //
        // POST: /Category/CategoryCreate

        [HttpPost]
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

                return RedirectToAction("CategoryIndex");

            }

            return View(category);
        }

        //
        // GET: /Category/CategoryDelete

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
        // POST: /Category/CategoryDelete

        [HttpPost, ActionName("CategoryDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryDeleteConfirmed(int id)
        {
            Category category = dbcategories.Categories.Find(id);
            string pad = Server.MapPath("~/Images/Categories/" + category.Name);
            try
            {
                var a = from picture in db2.Picture
                        where picture.Category == category.Name
                        select picture;

                foreach (var element in a)
                {
                    db2.Picture.Remove(element);
                }

                db2.SaveChanges();
                
                Directory.Delete(pad);
            }
            catch
            {
                Json("The system was unable to delete the category: " + category.Name);
            }
            dbcategories.Categories.Remove(category);
            dbcategories.SaveChanges();
            return RedirectToAction("CategoryIndex");
        }
    }
}