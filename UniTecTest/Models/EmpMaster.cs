using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniTecTest.Models
{
    public class EmpMaster
    {
        public int id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string experinece { get; set; }
        public string skill { get; set; }
        public string salary { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public DateTime enterDate { get; set; }
        public int empMasterType { get; set; }
    }
}