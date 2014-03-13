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
    public class CartController : Controller
    {
        private CartContext db = new CartContext();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View(db.Cart.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            CartModels cartmodels = db.Cart.Find(id);
            if (cartmodels == null)
            {
                return HttpNotFound();
            }
            return View(cartmodels);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CartModels cartmodels)
        {
            if (ModelState.IsValid)
            {
                db.Cart.Add(cartmodels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cartmodels);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CartModels cartmodels = db.Cart.Find(id);
            if (cartmodels == null)
            {
                return HttpNotFound();
            }
            return View(cartmodels);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CartModels cartmodels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartmodels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartmodels);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CartModels cartmodels = db.Cart.Find(id);
            if (cartmodels == null)
            {
                return HttpNotFound();
            }
            return View(cartmodels);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartModels cartmodels = db.Cart.Find(id);
            db.Cart.Remove(cartmodels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private static int[] Split(string list) //Split het winkelwagentje die als string uit de DB komt
        {
            string[] Splitted = list.Split(new char[] { '|' });
            int[] Splitted2 = new int[Splitted.Length];
            for (int i = 0; i < Splitted.Length; i++)
            {
                Splitted2[i] = Convert.ToInt32(Splitted[i]);
            }
            return Splitted2;
        }
        private static string Merge(int[] list) //Merged het winkelwagentje om hem daarna in de database te stoppen
        {
            return String.Join("|", list);
        }
    }
}