using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UniTecTest.Context;
using UniTecTest.Models;

namespace UniTecTest.ExtraClasses
{
    public class ObjectAssignAndSave
    {
        private static EmpMaster _empMaster;
        // This is the static method that controls the access to the singleton

        public static EmpMaster GetInstanceEmpMaster()// Get instance object admin
        {
            if (_empMaster == null)
            {
                _empMaster = new EmpMaster();

            }
            return _empMaster;
        }

        public static void SaveObject(EmpMaster empMaster) //admin object save database
        {
            using (UniTechTestContext db = new UniTechTestContext())// database object using DataAccessLayer , we define using becouse still runing project change database value show
            {
                db.empMasters.Add(empMaster);
                db.SaveChanges();
            }
        }
        public static void SaveObject(EmployerLeave employerLeave) //admin object save database
        {
            using (UniTechTestContext db = new UniTechTestContext())// database object using DataAccessLayer , we define using becouse still runing project change database value show
            {
                db.employerLeaves.Add(employerLeave);
                db.SaveChanges();
            }
        }
        public static void UpdateObject(EmpMaster empMaster) //admin object save database
        {
            using (UniTechTestContext db = new UniTechTestContext())// database object using DataAccessLayer , we define using becouse still runing project change database value show
            {
                db.empMasters.Attach(empMaster);
                db.Entry(empMaster).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void UpdateObject(EmployerLeave employerLeave) //admin object save database
        {
            using (UniTechTestContext db = new UniTechTestContext())// database object using DataAccessLayer , we define using becouse still runing project change database value show
            {
                db.employerLeaves.Attach(employerLeave);
                db.Entry(employerLeave).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static EmpMaster getObjectDatabaseEmpMaster(EmpMaster tempmaster, string condition) //admin object save database
        {
            using (UniTechTestContext db = new UniTechTestContext())// database object using DataAccessLayer , we define using becouse still runing project change database value show
            {
                if(condition == "get_login_field")
                {
                    tempmaster = db.empMasters.Where(d => d.email == tempmaster.email && d.password == tempmaster.password && d.confirmPassword == tempmaster.confirmPassword).FirstOrDefault();
                }
            }
            return tempmaster;
        }
    }
}