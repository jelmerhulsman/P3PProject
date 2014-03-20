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

namespace Jan_die_alles_kan.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/

        [HttpPost]
        public ActionResult Filter()
        {
            return Json("ABC");
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
