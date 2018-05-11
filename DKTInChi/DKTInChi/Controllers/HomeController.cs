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
        
        public ActionResult Index()
        {
            loadData();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void loadData()
        {
//            SqlDataReader reader = ;
            string conf = WebConfigurationManager.ConnectionStrings["DangKyTinChi"].ConnectionString;
            SqlConnection conn = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("SELECT * FROM student",conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string field1 = (string) reader["code"];
                    string field2 = (string) reader["lastname"];
                    Console.Write(field1 + ": " + field2 + "\n");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}