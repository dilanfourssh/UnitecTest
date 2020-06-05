using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniTecTest.Models
{
    public class EmployerLeave
    {
        public int id { get; set; }
        public string leaveType { get; set; }
        public int EmployerNo { get; set; }
        public DateTime applyDate { get; set; }
        public DateTime leaveDate { get; set; }
        public string numberOfDay { get; set; }
        public string reason { get; set; }
        public int confirmationType { get; set; }
    }
}