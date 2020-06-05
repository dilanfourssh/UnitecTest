using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniTecTest.Context;
using UniTecTest.ExtraClasses;
using UniTecTest.Models;

namespace UniTecTest.Controllers
{
    public class HomeController : Controller
    {
        private UniTechTestContext db = new UniTechTestContext();//create object my db;

        public ActionResult Index()
        {      
            db.empMasters.ToList();//Becouse call to db ,run index method auto create tables,and Protect By Sql Injections
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(EmpMaster empMaster)
        {
            empMaster = common.concatAndHashTempMaster(empMaster);//name and password concat and converting sha256

            if (common.validUser(empMaster))
            {
               empMaster = ObjectAssignAndSave.getObjectDatabaseEmpMaster(empMaster, "get_login_field");
                //assign session value
                Session["id"] = empMaster.id;
                Session["name"] = empMaster.email;
                Session["type"] = empMaster.empMasterType;
                //end
                if(empMaster.empMasterType == 1)
                {
                    return RedirectToAction("Administrator", "Home");
                }
                else
                { 
                    return RedirectToAction("AddDetils", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

            
        }

        // user section
        public ActionResult AddDetils()
        {
            
            if (Session["id"] != null)
            {
                int id =Convert.ToInt32( Session["id"]);
                var PreviousTimeUpdatedcheck = db.empMasters.Where(d => d.id == id).FirstOrDefault();
                id = 0;
                if(PreviousTimeUpdatedcheck.mobile == null && PreviousTimeUpdatedcheck.address == null)
                {
                    ViewBag.updated = true;
                }
                else
                {
                    ViewBag.updated = false;
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddDetils(EmpMaster empMaster)
        {
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                var PreviousTimeUpdatedcheck = db.empMasters.Where(d => d.id == id).FirstOrDefault();
                id = 0;
                if(PreviousTimeUpdatedcheck.mobile == null && PreviousTimeUpdatedcheck.address == null)//previous Enter Data Not Update Back end check
                {
                    //current Object Value Assign
                    empMaster.email = PreviousTimeUpdatedcheck.email;
                    empMaster.position = PreviousTimeUpdatedcheck.position;
                    empMaster.salary = PreviousTimeUpdatedcheck.salary;
                    empMaster.id = PreviousTimeUpdatedcheck.id;
                    empMaster.enterDate = DateTime.Now;
                    empMaster = common.concatAndHashTempMaster(empMaster);
                    //end
                    ObjectAssignAndSave.UpdateObject(empMaster);
                }
                return RedirectToAction("AddDetils", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult LeaveSection()
        {

            if (Session["id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult LeaveSection(EmployerLeave employerLeave)
        {
            if (Session["id"] != null)
            {
                employerLeave.applyDate = DateTime.Now;
                employerLeave.confirmationType = 0;
                ObjectAssignAndSave.SaveObject(employerLeave);
                return RedirectToAction("MyLeave", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult MyLeave()
        {
            if (Session["id"] != null)
            {
                int myid = Convert.ToInt32(Session["id"]);
                ViewBag.myleave = db.employerLeaves.Where(d => d.EmployerNo == myid && d.confirmationType == 0).OrderByDescending(d=>d.id).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult MyLeave(EmployerLeave employerLeave)
        {
            if (Session["id"] != null)
            {
                employerLeave.applyDate = DateTime.Now;
                employerLeave.confirmationType = 0;
                ObjectAssignAndSave.UpdateObject(employerLeave);
                return RedirectToAction("MyLeave", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult ConfirmLeave()
        {
            if (Session["id"] != null )
            {
                int myid = Convert.ToInt32(Session["id"]);
                ViewBag.myleave = db.employerLeaves.Where(d => d.EmployerNo == myid && d.confirmationType == 2).OrderByDescending(d=>d.id).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult DeleteLeave(int leaveid)
        {
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                var employerLeave = db.employerLeaves.Where(d=>d.id==leaveid && d.EmployerNo == id && d.confirmationType == 0).FirstOrDefault();
                employerLeave.confirmationType = 3;
                employerLeave.applyDate = DateTime.Now;
                ObjectAssignAndSave.UpdateObject(employerLeave);
                return RedirectToAction("MyLeave", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        // End User Sections

        // admin Sections
        public ActionResult Administrator()
        {
            if (Session["id"] != null && Convert.ToInt32(Session["type"]) == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult Administrator(EmpMaster empMaster)
        {
            if (Session["id"] != null && Convert.ToInt32(Session["type"]) == 1)
            {
                if(empMaster != null)
                {
                    empMaster.enterDate = DateTime.Now;
                    empMaster.empMasterType = 0;
                    empMaster = common.concatAndHashTempMaster(empMaster);
                    ObjectAssignAndSave.SaveObject(empMaster);
                    
                }
                return RedirectToAction("Administrator", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult ShowLeaveForm()
        {
            if (Session["id"] != null && Convert.ToInt32(Session["type"]) == 1)
            {
                ViewBag.myleave = db.employerLeaves.Where(d => d.confirmationType == 0).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult AdminDesition(int leaveid,string decision)
        {
            if (Session["id"] != null && Convert.ToInt32(Session["type"]) == 1)
            {
                
                var employerLeave = db.employerLeaves.Where(d => d.id == leaveid && d.confirmationType == 0).FirstOrDefault();
                if(decision == "con")
                {
                    employerLeave.confirmationType = 2;
                }
                else
                {
                    employerLeave.confirmationType = 3;
                }
                employerLeave.applyDate = DateTime.Now;
                ObjectAssignAndSave.UpdateObject(employerLeave);
                return RedirectToAction("ShowLeaveForm", "Home");
                
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        //end Admin Sections
        public ActionResult SignOut()
        {
            Session["id"] = null;
            Session["name"] = null;
            Session["type"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
