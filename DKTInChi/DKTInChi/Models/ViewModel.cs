using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKTInChi.Models
{
    public class ViewModel
    {
        public IPagedList<Subject> subjects { get; set; }
        public List<Subject> list_sub { get; set; }
        public List<Subject> list_registered { get; set; }
    }
}