using System.Web.Mvc;
using System.Data.SqlClient;
using DKTInChi.Models;
using System.Collections.Generic;

namespace DKTInChi.Controllers
{
    public class RegisterController : Controller
    {

        public List<Subject> listSubject = new List<Subject>();

        // GET
        public ActionResult Index()
        {
            loadData();
            return View(model: listSubject);
        }

        public ActionResult RegisterAction(string code)
        {
            return RedirectToAction("Index");
        }

        private void loadData()
        {
            SqlDataReader reader = CommonFunction.CommonFunction.loadData("SELECT * FROM subjects");
            while (reader.Read())
            {
                string code = (string)reader["code"];
                string name = (string)reader["name"];
                int numbercredit = (int)reader["numbercredit"];
                int theory = (int)reader["theory"];
                int totalsession = (int)reader["totalsession"];
                Subject subject = new Subject
                {
                    code = code,
                    name = name,
                    numbercredit = numbercredit,
                    theory = theory,
                    totalsession = totalsession
                };
                listSubject.Add(subject);
            }
        }
    }
}