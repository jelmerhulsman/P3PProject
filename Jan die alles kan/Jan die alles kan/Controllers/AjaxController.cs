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
        public ActionResult GetPhotos() {
            PicturesContext pContext = new PicturesContext();
            return Json(pContext.Picture.ToList());
        }

        [HttpPost]
        public ActionResult Filter(FormCollection collection)
        {
            PicturesContext pContext = new PicturesContext();
            List<string> aColors = new List<string>();
            List<string> aCategories = new List<string>();
            var pModel = pContext.Picture;

            string sColors = collection["colors"]; // red, yellow
            if (sColors != "")
            {
                aColors = sColors.Split(',').ToList();
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
            
            if (sColors != "") {
                pictures = from p in pictures
                           where aColors.Contains(p.Color)
                           select p;
            }

            if (sCategories != "")
            {
                pictures = from p in pictures
                           where aCategories.Contains(p.Category)
                           select p;
            }

            return Json(pictures);
        }

        [HttpPost]
        public string FormLogin(FormCollection collection)
        {
            CustSecurityController Secure = new CustSecurityController();

            try
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            }
            catch { }

            WebSecurity.Logout();

            if (WebSecurity.Login(collection["username"], collection["password"], persistCookie: false))
            {
                if (User.IsInRole("Admin"))
                {
                    if (CustSecurity.IPCheck(Secure.Details(collection["username"]), Request.UserHostAddress))
                    {
                        return "ad";
                    }
                    else
                    {
                        WebSecurity.Logout();
                        Secure.createIPVerification(new IPProfile(collection["username"], Request.UserHostAddress));
                        return "de";
                    }
                }
                else
                {
                    return "us";
                }
            }

            // If we got this far, something failed, redisplay form
            return "fa";
        }

        [HttpPost]
        public ActionResult PhotoInfo(FormCollection collection)
        {
            int id = Convert.ToInt16(collection["id"]);
            PicturesContext pContext = new PicturesContext();
            PictureModel photo = pContext.Picture.Find(id);

            return Json(photo);
        }

        [HttpPost]
        public ActionResult PhotoToCart(FormCollection collection)
        {
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
                return Json("Te system was unable to save your order");
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
                return Json("Te system was unable to save your order");
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
