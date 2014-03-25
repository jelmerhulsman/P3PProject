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
using System.IO;
using System.Globalization;

namespace Jan_die_alles_kan.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        /// <summary>
        /// Haalt data op vanuit database en vult de pagina hiermee, dit correspondeert met de gebruiker die ingelogd is.
        /// </summary>
        /// <returns>The modified view</returns>
        public ActionResult DownloadPage()
        {
            UserDataContext udc = new UserDataContext();
            PicturesContext pc = new PicturesContext();
            PictureModel pm = new PictureModel();

            var a = from user in udc.DBUserData
                    where user.Username == User.Identity.Name
                    select user.Order;
            char[] order = a.ToArray().First().ToArray();
            string tempstr ="";
            List<int> orderList = new List<int>();
            for (int i = 0; i < order.Count(); i++)
            {
                
                if (char.IsNumber(order[i]))
                {
                    
                    string temp = Convert.ToString(order[i]);
                    tempstr += temp;
                }
                else
                {
                    if (tempstr.Length > 0)
                    {
                      orderList.Add(Convert.ToInt32(tempstr));
                    tempstr = "";  
                    }
                    

                }
                if (i+1 == order.Count())
                {
                    orderList.Add(Convert.ToInt32(tempstr));
                }
            }
            List<PictureModel> photoList = new List<PictureModel>();
            foreach (int ID in orderList)
            {
                var photo = from x in pc.Picture
                            where x.Id == ID
                            select x;
                photoList.Add(photo.ToList().First());
            }
            ViewData["photoList"] = photoList;
            return View("downloadpage");
        }

        /// <summary>
        /// The image download function
        /// </summary>
        /// <param name="category">The category of the image</param>
        /// <param name="file_name">The filename you're downloading</param>
        /// <returns>A image download</returns>
        public FileResult FileDownloadPage(string category, string file_name)
        {
            FileResult result;
            string path = Server.MapPath("~/Images/Categories/" + category + "/" +file_name+".jpg");
            using (FileStream stream = System.IO.File.OpenRead(path))
            {
                MemoryStream streamFile = GetFile(path);
                result = new FileContentResult(streamFile.ToArray(), "image/jpeg");
                result.FileDownloadName = "image.jpg";
                
            }
            return result;
        }

        /// <summary>
        /// Gets the information of the file so it can be downloaded
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <returns>A memorystream for file downloading</returns>
        MemoryStream GetFile(string path)
        {
            byte[] buffer = new byte[4096];
            FileStream fs;
            MemoryStream streamFile = new MemoryStream();
            using (fs = System.IO.File.OpenRead(path))
            {
                int sourceBytes;
                do
                {
                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
   	                streamFile.Write(buffer,0,sourceBytes);
	            } 
                while (sourceBytes > 0);
            }
            return streamFile;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        /// <summary>
        /// Fills the checkout page with data from the user' cart.
        /// </summary>
        /// <returns>The modifed checkout view</returns>
        public ActionResult Checkout()
        {
            UserDataContext udc = new UserDataContext();
            PicturesContext pc = new PicturesContext();
            PictureModel pm = new PictureModel();

            var a = from user in udc.DBUserData
                    where user.Username == User.Identity.Name
                    select user.Order;
            char[] order = a.ToArray().First().ToArray();
            string tempstr = "";
            List<int> orderList = new List<int>();
            for (int i = 0; i < order.Count(); i++)
            {

                if (char.IsNumber(order[i]))
                {

                    string temp = Convert.ToString(order[i]);
                    tempstr += temp;
                }
                else
                {
                    if (tempstr.Length > 0)
                    {
                        orderList.Add(Convert.ToInt32(tempstr));
                        tempstr = "";
                    }


                }
                if (i + 1 == order.Count())
                {
                    orderList.Add(Convert.ToInt32(tempstr));
                }
            }
            List<PictureModel> photoList = new List<PictureModel>();
            foreach (int ID in orderList)
            {
                var photo = from x in pc.Picture
                            where x.Id == ID
                            select x;
                if(photo.Any())
                photoList.Add(photo.ToList().First());
            }
            ViewData["photoList"] = photoList;
            return View("Checkout");
        }
        /// <summary>
        /// When the "Buy" button is clicked, it will redirect to either paypal or ideal.
        /// </summary>
        /// <param name="form">The form information that is being passed in</param>
        /// <returns>A redirect to either paypal or ideal</returns>
        [HttpPost]
        public ActionResult CheckoutConfirmed(FormCollection form)
        {
            string PaymentOption = form["paymentmethod"];
            string urlredirect = "https://www.paypal.com/";
            if (PaymentOption == "Ideal")
                urlredirect = "http://www.ideal.nl";

            return Redirect(urlredirect);
        }

        /// <summary>
        /// The holy grail of our login system, when clicked on "login" it will authorize the user and logs the user in.
        /// </summary>
        /// <param name="model">The model what we're working with</param>
        /// <returns>This will login the user</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            CustSecurityController Secure = new CustSecurityController();
            WebSecurity.Logout();

            //IP Check
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                if (Roles.IsUserInRole(model.UserName, "Admin"))
                {
                    if (CustSecurity.IPCheck(Secure.Details(model.UserName), Request.UserHostAddress))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        WebSecurity.Logout();
                        Secure.createIPVerification(new IPProfile(model.UserName, Request.UserHostAddress));
                        ModelState.AddModelError("", "IP is not certified, an email has been sent to your account");
                        return View("~/Views/Account/Login.aspx", model);
                    }
                }

                // If we got this far, admin login failed, so you are just regular user
                return RedirectToAction("Overview", "Page");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View("~/Views/Account/Login.aspx", model);
        }

        /// <summary>
        /// Simply, logs the user off.
        /// </summary>
        /// <returns>""</returns>
        public ActionResult LogOff()
        {
            // Ingelogde gebruiker ophalen
            UserDataContext uContext = new UserDataContext();
            UserData user;
            string userName = User.Identity.Name;
            var a = from x in uContext.DBUserData
                    where x.Username == userName
                    select x;
            user = a.ToList().First();

            user.Order = null;

            try
            {
                uContext.SaveChanges();
            }
            catch (Exception e)
            {
                return Json("The system was unable to save your order");
            }

            Session["order"] = null;

            WebSecurity.Logout();

            return RedirectToAction("Overview", "Page");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// When clicked on the "register" button, it will execute this method. Which will register the user with the data he has provided in the database.
        /// </summary>
        /// <param name="model">The model what we're working with</param>
        /// <returns>The normal overview page / error message</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    EmailAddressAttribute emailCheck = new EmailAddressAttribute();

                    if (emailCheck.IsValid(model.Email))
                    {
                        UserDataContext db = new UserDataContext();

                        CustSecurityController Secure = new CustSecurityController();
                        WebSecurity.CreateUserAndAccount(model.UserName, model.Password);

                        UserData dataProfile = new UserData(model.UserName, model.Email, model.Street, model.HouseNumber, model.City, model.PostalCode, null);

                        db.DBUserData.Add(dataProfile);
                        db.SaveChanges();

                        Secure.Create(new IPProfile(model.UserName, Request.UserHostAddress));
                        WebSecurity.Login(model.UserName, model.Password);
                        return RedirectToAction("Overview", "Page");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The email address entered is not valid");
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        /// <summary>
        /// Used to disassociate the user with the website
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="providerUserId"></param>
        /// <returns>The manage account screen</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        /// <summary>
        /// Fills the manage page with data so it can be used to change password of the current user.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>The modified view</returns>
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        /// <summary>
        /// When clicked on "submit" it will execute this method, which will check if all the data which is being filled in by the user is correct and if so, will change the password of the user.
        /// </summary>
        /// <param name="model">The model what we're working with</param>
        /// <returns>Either the same view with errors displayed, or it will redirect back to the manage screen.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", String.Format("Unable to create local account. An account with the name \"{0}\" may already exist.", User.Identity.Name));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
