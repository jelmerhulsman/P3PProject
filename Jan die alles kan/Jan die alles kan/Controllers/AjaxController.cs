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
            PicturesContext pContext = new PicturesContext();
            PictureModel photo = pContext.Picture.Find(id);

            var order = Session["order"];
            if (order == null)
            {
                Session["order"] = id.ToString();
            }
            else
            {
                Session["order"] = order + ", " + id;
            }

            return Json(photo);
        }

        public ActionResult GetOrder()
        {
            var order = Session["order"];

            return Json(order);
        }
    }
}
