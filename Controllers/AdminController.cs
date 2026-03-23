using HealthCare_ProtoType.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthCare_ProtoType.Controllers
{
    public class AdminController : Controller
    {
        healthcaredbEntities adb = new healthcaredbEntities();
        // GET: Admin

        public ActionResult Index()
        {
            // Dashboard
            return View(adb);
        }

        public ActionResult ManageEnquiry()
        {
            //List<enquirymaster> enm = adb.enquirymasters.OrderByDescending(x=>x.EnquiryId)ToList();
            List<enquirymaster> enm = adb.enquirymasters.ToList();
            return View(enm);

        }

        public ActionResult DeleteEnquiry(int id)
        {
            string msg = "";
            try
            {
                enquirymaster den = adb.enquirymasters.Find(id);
                if (den != null)
                {
                    adb.enquirymasters.Remove(den);
                    adb.SaveChanges();
                    msg = "Record deleted Successfully.";
                }
                else
                {
                    msg = " X ....Record deleted unsuccess.....!!!";

                }
            }
            catch
            {
                msg = "Sorry....Technical Issue occurs.";
            }
            ViewBag.TempData["Message"] = msg;
            return RedirectToAction("ManageEnquiry","Admin");
                                        
        }
        public ActionResult Notification()
        { 
            return View();
        }
        [HttpPost]

        public ActionResult Notification(notificationmaster nm)
        {
            string msg = "";
            try
            {
                nm.AddedOn = DateTime.Now;
                adb.notificationmasters.Add(nm);
                adb.SaveChanges();
                msg = "Notification Added Successfully....!!";
            }
            catch
            {
                msg = "Notification Not Added....!!";
            }
            ViewBag.Message = msg;
            return View();
        }

        public ActionResult ManageNotification()
        {
            List<notificationmaster> nmM = adb.notificationmasters.ToList();
            return View(nmM);
        }

        public ActionResult DeleteNotice(int id)
        {
            string msg = "";
            try
            {


                notificationmaster nmM = adb.notificationmasters.Find(id);
                if (nmM != null)
                {
                    adb.notificationmasters.Remove(nmM);
                    adb.SaveChanges();
                    msg = "Record deleted successsfully";
                }
            }
            catch
            {
                msg = "Sorry!, Technical issue occured";
            }
            TempData["Message"] = msg;
            return RedirectToAction("ManageNotification");

        }

        public ActionResult ManageDoctors()
        {
            List<doctormaster> lst = adb.doctormasters.ToList();
            return View(lst);
        }

        public ActionResult AddDoctors()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDoctors(doctormaster dm)
        {
            string msg = "";
            try
            {
                dm.AddedOn = DateTime.Now;
                adb.doctormasters.Add(dm);
                adb.SaveChanges();
                msg = "Doctor data Added Successfully....!!";
            }
            catch
            {
                msg = "Doctor data Not Added....!!";

            }
            ViewBag.DocMessage = msg;
            return View();
        }
        public ActionResult DeleteDoctors(int id)
        {
            string msg = "";
            try
            {


                doctormaster em = adb.doctormasters.Find(id);
                if (em != null)
                {
                    adb.doctormasters.Remove(em);
                    adb.SaveChanges();
                    msg = "Record deleted successsfully";
                }
            }
            catch
            {
                msg = "Sorry!, Technical issue occured";
            }
            TempData["Message"] = msg;
            return RedirectToAction("ManageDoctors");
        }
    }
}