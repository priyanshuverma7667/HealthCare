using HealthCare_ProtoType.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace HealthCare_ProtoType.Controllers
{
    
    public class GeneralController : Controller
    {
        healthcaredbEntities db = new healthcaredbEntities();

        // GET: General
        public ActionResult Index()
        {
            //to show notification
            List<notificationmaster>

            lst = db.notificationmasters.OrderByDescending(x => x.NotiId).Take(3).ToList();
            return View(lst);
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Doctors()
        {
            List<doctormaster> lst = db.doctormasters.ToList();
            return View(lst);
        }

        [HttpPost]
        public ActionResult SaveEnquiry(enquirymaster em)
        {
            string msg = "";
            try
            {
                em.EnquiryDT = DateTime.Now;
                db.enquirymasters.Add(em);
                db.SaveChanges();
                msg = "Thanks for Enquiry Us.We will contact you soon.";
                //   EMailer eM = new EMailer();
                //eM.EnqMessage = "Thanks for your enquiry.we will contact you soon"; 
                //eM.SendMyEmail(eM.EmailId, em.Message);
            }
            catch
            {
                msg = "Sorry...! Some Technical issued Occured.";
            }
            TempData["Message"] = msg;
            return RedirectToAction("ResponseEnquiry");
        }
        public ActionResult ResponseEnquiry()
        {
            return View();
        }

        public ActionResult AdminLogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(adminmaster lm)
        {
            adminmaster lmdb = db.adminmasters.SingleOrDefault(x => x.AdminId == lm.AdminId && x.Password == lm.Password);
            if (lmdb != null)
            {
                Session["Aid"] = lmdb.AdminId;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Message = "Invalid User Id or Pssword";
            }
                return View();

        }

        public ActionResult DeverloperTeam()
        {
            return View();
        }
    }
}