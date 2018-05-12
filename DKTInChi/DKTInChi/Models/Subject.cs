using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKTInChi.Models
{
    public class Subject
    {
        public string code { get; set; }
        public string name { get; set; }
        public int numbercredit { get; set; }
        public int theory { get; set; }
        public int task { get; set; }
        public int pratice { get; set; }
        public int discussion { get; set; }
        public int totalsession { get; set; }
    }
}