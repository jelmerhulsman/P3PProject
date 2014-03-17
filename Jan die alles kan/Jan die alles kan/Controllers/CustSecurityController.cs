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

        public ActionResult Index()
        {
            return View(db.IPProfiles.ToList());
        }

        public IPProfile[] Details()
        {
            IPProfile[] ipprofile = db.IPProfiles.ToArray<IPProfile>();
            if (ipprofile == null)
            {
                return null;
            }
            return ipprofile;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public ActionResult Edit(int id = 0)
        {
            IPProfile ipprofile = db.IPProfiles.Find(id);
            if (ipprofile == null)
            {
                return HttpNotFound();
            }
            return View(ipprofile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public ActionResult Delete(int id = 0)
        {
            IPProfile ipprofile = db.IPProfiles.Find(id);
            if (ipprofile == null)
            {
                return HttpNotFound();
            }
            return View(ipprofile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IPProfile ipprofile = db.IPProfiles.Find(id);
            db.IPProfiles.Remove(ipprofile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public void createIPVerification(IPProfile ipProfile)
        {
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

            //Hier klopt ie nog niet, moet de juiste link zijn
            //MailController.SendMailInner(ipProfile.Username, "IP verificatie", "Hallo, er is geprobeert om op uw account in te loggen vanaf een IP dat niet eerder hiervoor is gebruikt. Als u dit was kunt u op de link hieronder klikken. Was u dit niet, dan hoeft u niks te doen. " + "http//milanov.tk/" + validationString);
        }

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

        public ActionResult test(int id = 0)
        {
            if (CheckAddIP(Convert.ToString(id)))
            {
                return Redirect("/account/login");
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}