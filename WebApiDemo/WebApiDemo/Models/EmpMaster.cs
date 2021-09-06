using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    public class EmpMaster
    {
        public int EmpCode { get; set; }
        public string EmpName { get; set; }
        public DateTime EmpDob { get; set; }
        public string Email { get; set; }
        public int DeptCode { get; set; }
    }
}