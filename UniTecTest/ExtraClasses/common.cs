using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniTecTest.Context;
using UniTecTest.Models;

namespace UniTecTest.ExtraClasses
{

    public class common
    {
        private UniTechTestContext db = new UniTechTestContext();//create object my db;

        public static EmpMaster concatAndHashTempMaster(EmpMaster tempmaster)
        {
           string hash = tempmaster.email + tempmaster.password;
           tempmaster.password = SHA.GenerateSHA256String(hash);

           hash = tempmaster.email + tempmaster.password;
           tempmaster.confirmPassword = SHA.GenerateSHA256String(hash);

           return tempmaster;
        }
        public static Boolean validUser(EmpMaster tempmaster)
        {
            Boolean checkvalid = false;
            using (UniTechTestContext db = new UniTechTestContext())// database object using DataAccessLayer , we define using becouse still runing project change database value show
            {
                tempmaster = ObjectAssignAndSave.getObjectDatabaseEmpMaster(tempmaster,"get_login_field");
                if(tempmaster != null)
                {
                    checkvalid = true;
                }
            }
                
            return checkvalid;
        }
    }
}