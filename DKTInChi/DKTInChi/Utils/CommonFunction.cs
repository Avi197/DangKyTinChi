using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace CommonFunction
{
    public static class CommonFunction
    {
        public static SqlDataReader loadData(string command)
        {
            string conf = WebConfigurationManager.ConnectionStrings["DangKyTinChi"].ConnectionString;
            SqlConnection conn = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand(command, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public static void execNonQuery(string command)
        {
            string conf = WebConfigurationManager.ConnectionStrings["DangKyTinChi"].ConnectionString;
            SqlConnection conn = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand(command, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}