using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace DKTInChi.Controllers
{
    public class HomeController : Controller
    {
        public bool CheckLogin()
        {
            if (Session["username"] == null)
            {
                return false;
            }
            else return true;
        }
        public ActionResult Index()
        {
            if (CheckLogin() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                loadData();
                return View();
            }
        }

        private void loadData()
        {

            SqlDataReader reader = CommonFunction.CommonFunction.loadData("SELECT * FROM student");
            while (reader.Read())
            {
                string field1 = (string)reader["code"];
                string field2 = (string)reader["lastname"];
                Console.Write("student: " + field1 + ": " + field2 + "\n");
            }
        }
    }
}