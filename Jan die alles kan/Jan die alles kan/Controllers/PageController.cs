﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jan_die_alles_kan.Models;

namespace Jan_die_alles_kan.Controllers
{
    public class PageController : Controller
    {

        private PagesContext db = new PagesContext();
        private PicturesContext db_pictures = new PicturesContext();
        private CategoryContext db_category = new CategoryContext();
       
        public ActionResult Landingpage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Overview(FormCollection collection)
        {
            ViewData["pages"] = db.Pages.ToList();
            ViewData["pictures"] = db_pictures.Picture.ToList();

            string searchTerm = collection["search"].ToString();
            if (searchTerm != null)
            {
                var picture = from x in db_pictures.Picture
                              where x.Name.Contains(searchTerm)
                              select x;
                ViewData["pictures"] = picture;
            }           

            var categories = from c in db_category.Categories
                             orderby c.Name
                             select c;
            ViewData["categories"] = categories.ToList();

            return View();
        }

        public ActionResult Overview()
        {
            ViewData["pages"] = db.Pages.ToList();
            ViewData["pictures"] = db_pictures.Picture.ToList();
            var categories = from c in db_category.Categories
                             orderby c.Name
                             select c;
            ViewData["categories"] = categories.ToList();

            return View();
        }

        public ActionResult Content(string Id = "")
        {
            PagesModels pagemodel;

            var page = from p in db.Pages
                       where p.Permalink == Id
                       select p;

            try
            {
                pagemodel = page.ToArray().First();
            }
            catch
            {
                return HttpNotFound();
            }

            pagemodel.Content = Shortcodes.ShortSplitter.ShortReplace(pagemodel.Content);

            ViewBag.Name = pagemodel.Name;
            ViewBag.Content = pagemodel.Content;
            ViewData["pages"] = db.Pages.ToList();
            return View(ViewBag);
        }

        public ActionResult BackToDashboard()
        {
            return RedirectToAction("Dashboard/Index");
        }

        public ActionResult Details(int Id = 0)
        {
            PagesModels pagemodel = db.Pages.Find(Id);
            
            if (pagemodel == null)
            {
                return HttpNotFound();
            }

            pagemodel.Content = Shortcodes.ShortSplitter.ShortReplace(pagemodel.Content);

            //ViewBag.Content = pagemodel.Content;
            return View(pagemodel);
             
            //return View("Details");
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}