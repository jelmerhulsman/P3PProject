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
    public class CategoryController : Controller
    {
        private CategoryContext db = new CategoryContext();

        //
        // GET: /Category/

        public ActionResult CategoryIndex()
        {
            return View(db.Categories.ToList());
        }

        //
        // GET: /Category/CategoryDetails/5

        public ActionResult CategoryDetails(int id = 0)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
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
                db.Categories.Add(category);
                db.SaveChanges();
                string pad = Server.MapPath("~/Images/Categories/" + category.Name);
                Directory.CreateDirectory(pad);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        //
        // GET: /Category/CategoryEdit/5

        public ActionResult CategoryEdit(int id = 0)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/CategoryEdit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit(Category category)
        {
            if (ModelState.IsValid)
            {
                Category previous = db.Categories.Find(category.Id);
                db.Categories.Remove(previous);
                db.Categories.Add(category);
                db.SaveChanges();
                
                string pad = Server.MapPath("~/Images/Categories/" + category.Name);
                string oldPad = Server.MapPath("~/Images/Categories/" + previous.Name);
                Directory.Move(oldPad, pad);

                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/CategoryDelete/5

        public ActionResult CategoryDelete(int id = 0)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/CategoryDelete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryDeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            string pad = Server.MapPath("~/Images/Categories/" + category.Name);

            Directory.Delete(pad);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}