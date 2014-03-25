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
    public class CustSecurityController : Controller
    {
        private CustSecurityContext db = new CustSecurityContext();

        /// <summary>
        /// Returns a list of verified IP's
        /// </summary>
        /// <returns>List of verified IP's</returns>
        [Authorize (Roles="Admin")]
        public ActionResult Index()
        {
            return View(db.IPProfiles.ToList());
        }
        /// <summary>
        /// Gives all the verified IP's tied to a certain username
        /// </summary>
        /// <param name="username">The username which's IP's you are looking for</param>
        /// <returns>An array with all the verified IP's that go with a certain username</returns>
        [Authorize(Roles = "Admin")]
        public string[] Details(string username)
        {
            var intermediate = from profile in db.IPProfiles
                               where profile.Username == username
                               select profile.IP;

            string[] ipArray = intermediate.ToArray();
            if (ipArray == null)

            {
                return null;
            }
            return ipArray;
        }

        /// <summary>
        /// Create view
        /// </summary>
        /// <returns>Create form</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create function for an IPProfile
        /// </summary>
        /// <param name="ipprofile">The IPProfile you want to create</param>
        /// <returns>The IPProfile view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(IPProfile ipprofile)
        {
            if (ModelState.IsValid)
            {
                db.IPProfiles.Add(ipprofile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ipprofile);
        }

        /// <summary>
        /// This function checks if a certain IPProfile can be edited
        /// </summary>
        /// <param name="id">The id of the IPProfile you want to change</param>
        /// <returns>The IPProfile edit view</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            IPProfile ipprofile = db.IPProfiles.Find(id);
            if (ipprofile == null)
            {
                return HttpNotFound();
            }
            return View(ipprofile);
        }

        /// <summary>
        /// This function edits a certain IPProfile
        /// </summary>
        /// <param name="ipprofile">The id of the IPProfile you want to change</param>
        /// <returns>The IPProfile edit view or the indexpage</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(IPProfile ipprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ipprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ipprofile);
        }

        /// <summary>
        /// This function checks if a certain IPProfile can be deleted
        /// </summary>
        /// <param name="id">The IPProfile you want to delete</param>
        /// <returns>The delete view</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            IPProfile ipprofile = db.IPProfiles.Find(id);
            if (ipprofile == null)
            {
                return HttpNotFound();
            }
            return View(ipprofile);
        }

        /// <summary>
        /// This function deletes an IPProfile
        /// </summary>
        /// <param name="id">The IPProfile you want to delete</param>
        /// <returns>To the index page</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            IPProfile ipprofile = db.IPProfiles.Find(id);
            db.IPProfiles.Remove(ipprofile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// This function creates a verification entry into the IPVerification table so that the user gets a link which wen clicked
        /// will add the new IP to the table with verified IP's
        /// </summary>
        /// <param name="ipProfile">The IPProfile you want to verify</param>
        [Authorize(Roles = "Admin")]
        public void createIPVerification(IPProfile ipProfile)
        {
            //Insert your domain here to be able to send correct email verification
            //Insert it like this: http://www.yourname.com
            string hostAdress = "";
            
            IPVerificationContext db = new IPVerificationContext();
            string validationString;

            for (int i = 0; true; i++)
            {
                validationString = GenerateCode();

                var codes = from profile in db.IPVerificationEntries
                            select profile.Code;

                string[] codeArray = codes.ToArray();

                if (!codeArray.Contains(validationString))
                {
                    break;
                }
            }

            IPVerificationModel verification = new IPVerificationModel(ipProfile.Username, ipProfile.IP, validationString);

            db.IPVerificationEntries.Add(verification);
            db.SaveChanges();
            if (hostAdress != "")
            {
                MailController.SendMailInner(ipProfile.Username, "IP verificatie", "Hallo, er is geprobeert om op uw account in te loggen vanaf een IP dat niet eerder hiervoor is gebruikt. Als u dit was kunt u op de link hieronder klikken. Was u dit niet, dan hoeft u niks te doen. " + hostAdress + "/test/" + validationString);
            }
        }

        /// <summary>
        /// This function generates a special verification code which is a random numeral code with a lenght of 30 numbers
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        private string GenerateCode()
        {
            string validationString = "";
            Random rnum = new Random();

            for (int i = 0; i < 30; i++)
            {
                int randomNumber = rnum.Next(0, 36);

                validationString = validationString + Convert.ToString(randomNumber);
            }

            return validationString;
        }

        /// <summary>
        /// This function checks for the code entered through the link the user got. If the code matches a code in the database,
        /// the corresponding IP will be added to the list of verified IP's
        /// </summary>
        /// <param name="code">The code you want to check for</param>
        /// <returns>a boolean value which indicates if the procedure was succesfull</returns>
        [Authorize(Roles = "Admin")]
        public bool CheckAddIP(string code)
        {
            IPVerificationContext db = new IPVerificationContext();

            var verification = from profile in db.IPVerificationEntries
                               where profile.Code == code
                               select profile;

            if (verification != null)
            {
                IPVerificationModel IPVerification = verification.ToArray().First();

                CustSecurityContext dbNew = new CustSecurityContext();

                IPProfile newProfile = new IPProfile(IPVerification.Username, IPVerification.IP);

                dbNew.IPProfiles.Add(newProfile);
                dbNew.SaveChanges();

                db.IPVerificationEntries.Remove(IPVerification);
                db.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// This is the public function which can be accessed without logging in, so users can verify their IP
        /// </summary>
        /// <param name="id">The code that you want to check for</param>
        /// <returns>If succesfull you will be redirected to the login page, otherwise to the landingpage</returns>
        public ActionResult test(int id = 0)
        {
            if (CheckAddIP(Convert.ToString(id)))
            {
                return Redirect("/Account/Login");
            }
            else
            {
                return Redirect("/Page/Overview");
            }
        }
    }
}