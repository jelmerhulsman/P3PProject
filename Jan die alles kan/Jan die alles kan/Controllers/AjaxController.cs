using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Jan_die_alles_kan.Filters;
using Jan_die_alles_kan.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Data.Objects.SqlClient;

namespace Jan_die_alles_kan.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/
        [HttpPost]
        public ActionResult GetPhotos(FormCollection collection) {
            PicturesContext pContext = new PicturesContext();
            
            string searchTerm = collection["searchTerm"];
            if (collection["searchTerm"] != "")
            {
                var pictures = from p in pContext.Picture
                               where p.Name.Contains(searchTerm)
                               orderby p.Id descending
                               select p;
                if (pictures.Count() == 0)
                {
                    return Json(false);
                }
                else {
                    return Json(pictures.ToList());
                }
            }
            else
            {
                var pictures = from p in pContext.Picture
                               orderby p.Id descending
                               select p;
                return Json(pictures.ToList());
            }
        }

        [HttpPost]
        public ActionResult OrderPhotos(FormCollection collection)
        {
            PicturesContext pContext = new PicturesContext();

            string sPicturesIds = collection["pictures"];
            string order = collection["order"];

            if (sPicturesIds != "")
            {
                List<string> aPicturesIds = sPicturesIds.Split(',').ToList();
                List<PictureModel> pictures = new List<PictureModel>();

                foreach (string i in aPicturesIds)
                {
                    int lookFor = Convert.ToInt32(i);

                    var picture = from x in pContext.Picture
                                  where x.Id == lookFor
                                  select x;

                    pictures.AddRange(picture);
                }

                IEnumerable<PictureModel> orderedPictures = pictures;

                switch (order)
                {
                    case "newest":
                        orderedPictures = pictures.OrderBy(PictureModel => PictureModel.Id);
                        break;
                    case "nameAZ":
                        orderedPictures = pictures.OrderBy(PictureModel => PictureModel.Name);
                        break;
                    case "nameZA":
                        orderedPictures = pictures.OrderByDescending(PictureModel => PictureModel.Name);
                        break;
                    case "priceLH":
                        orderedPictures = pictures.OrderBy(PictureModel => PictureModel.Price);
                        break;
                    case "priceHL":
                        orderedPictures = pictures.OrderByDescending(PictureModel => PictureModel.Price);
                        break;
                }

                return Json(orderedPictures);
            }

            return Json(false);
        }

        [HttpPost]
        public ActionResult Filter(FormCollection collection)
        {
            PicturesContext pContext = new PicturesContext();
            List<string> aColors = new List<string>();
            List<string> aOrientation = new List<string>();
            List<string> aCategories = new List<string>();
            var pModel = pContext.Picture;

            string sColors = collection["colors"]; // red, yellow
            if (sColors != "")
            {
                aColors = sColors.Split(',').ToList();
            }

            string sOrientation = collection["orientation"]; // a, b
            if (sOrientation != "")
            {
                aOrientation = sOrientation.Split(',').ToList();
            }

            string sCategories = collection["categories"]; // a, b
            if (sCategories != "")
            {
                aCategories = sCategories.Split(',').ToList();
            }

            string sPricerange = collection["priceRange"]; // €20 €30
            var aPricerange = sPricerange.Split(',');
            int priceMin = Convert.ToInt32(aPricerange[0]);
            int priceMax = Convert.ToInt32(aPricerange[1]);

            var pictures = from p in pModel
                           where p.Price >= priceMin && p.Price <= priceMax
                           select p;
            
            string sName = collection["name"];
            if (sName != "")
            {
                pictures = from p in pictures
                           where p.Name.Contains(sName)
                           select p;
            }

            if (sColors != "") 
            {
                pictures = from p in pictures
                           where aColors.Contains(p.Color)
                           select p;
            }

            if (sOrientation != "")
            {
                pictures = from p in pictures
                           where aOrientation.Contains(p.Orientation)
                           select p;
            }

            if (sCategories != "")
            {
                pictures = from p in pictures
                           where aCategories.Contains(p.Category)
                           select p;
            }

            var countPictures = pictures.Count();

            if (countPictures == 0)
            {
                return Json(false);
            }
            else
            {
                return Json(pictures);
            }
        }

        /// <summary>
        /// Informatie ophalen van de 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PhotoInfo(FormCollection collection)
        {
            //if (collection["id"].Contains(","))
            //    collection["id"].Remove(0, 1);
            int id = Convert.ToInt16(collection["id"]);
            PicturesContext pContext = new PicturesContext();
            PictureModel photo = pContext.Picture.Find(id);

            return Json(photo);
        }

        [HttpPost]
        public ActionResult PhotoToCart(FormCollection collection)
        {
            if (collection["id"].Contains(',') == true)
                collection["id"].Remove(0, 1);
            int id = Convert.ToInt16(collection["id"]);

            // Foto ophalen
            PicturesContext pContext = new PicturesContext();            
            PictureModel photo = pContext.Picture.Find(id);

            // Ingelogde gebruiker ophalen
            UserDataContext uContext = new UserDataContext();
            UserData user;
            string userName = User.Identity.Name;
            var a = from x in uContext.DBUserData
                    where x.Username == userName
                    select x;
            user = a.ToList().First();

            var order = Session["order"];
            if (order == null)
            {
                Session["order"] = id.ToString();
            }
            else
            {
                Session["order"] = order + ", " + id;
            }
            order = Session["order"];

            user.Order = order.ToString();

            try
            {
                uContext.SaveChanges();
            }
            catch (Exception e)
            {
                return Json("The system was unable to save your order");
            }

            return Json(photo);
        }

        [HttpPost]
        public ActionResult RemoveFromCart(FormCollection collection) {
            Session["order"] = collection["order"].ToString();

            // Ingelogde gebruiker ophalen
            UserDataContext uContext = new UserDataContext();
            UserData user;
            string userName = User.Identity.Name;
            var a = from x in uContext.DBUserData
                    where x.Username == userName
                    select x;
            user = a.ToList().First();

            user.Order = Session["order"].ToString();

            try
            {
                uContext.SaveChanges();
            }
            catch (Exception e)
            {
                return Json("The system was unable to save your order");
            }

            return Json(Session["order"]);
        }

        public ActionResult GetOrder()
        {
            var order = "";

            // Ingelogde gebruiker ophalen
            if (User.Identity.IsAuthenticated)
            {
                UserDataContext uContext = new UserDataContext();
                UserData user;
                string userName = User.Identity.Name;
                var a = from x in uContext.DBUserData
                        where x.Username == userName
                        select x;
                user = a.ToList().First();

                
                if (Session["order"] == null)
                {
                    order = user.Order;
                    Session["order"] = order;
                }
                else
                {
                    order = Session["order"].ToString();
                }
            }

            return Json(order);
        }
    }
}
