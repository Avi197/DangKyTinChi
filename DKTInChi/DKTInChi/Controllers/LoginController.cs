using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DKTInChi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Auth()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            int i = 0;

            SqlDataReader reader = CommonFunction.CommonFunction.loadData("exec Login '" + username + "','" + password+"'");
            while (reader.Read())
            {
                i = (int)reader["result"];
            }
            if (i == 1)
            {
                Session["username"] = username;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["login_error"] = "Username or password incorrect!";
                return View("Index");
            }
        }
    }
}