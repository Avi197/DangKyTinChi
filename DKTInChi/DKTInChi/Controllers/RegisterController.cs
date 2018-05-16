using System;
using System.Web.Mvc;
using System.Data.SqlClient;
using DKTInChi.Models;
using System.Collections.Generic;

namespace DKTInChi.Controllers
{
    public class RegisterController : Controller
    {

        
        public List<CourseClass> listCourseClasses = new List<CourseClass>();
        public List<RegisterClass> listRegisterClasses = new List<RegisterClass>();

        // GET
        public ActionResult Index()
        {
            loadData();
            loadResgister();
            ViewData["ListRegisterClasses"] = listRegisterClasses;
            return View(model: listCourseClasses);
        }
        
        public ActionResult RegisterAction(string maHocPhan)
        {
            string cmd = String.Format("exec Proc_DangKy '15150419', '"+ maHocPhan +"',250000,1");
            CommonFunction.CommonFunction.execNonQuery(cmd);
            TempData["msg"] = "<script>alert('Đăng ký thành công');</script>";
            return RedirectToAction("Index");
        }
        
        private void loadResgister()
        {
            listRegisterClasses.Clear();
            SqlDataReader readerRegister = CommonFunction.CommonFunction.loadData("exec inKQ '15150419'");
            while (readerRegister.Read())
            {
                RegisterClass rc = new RegisterClass
                {
                    maHocPhan = (string) readerRegister["MaHP"],
                    maLopHocPhan = (string) readerRegister["MaLHP"],
                    tenMonHoc = (string) readerRegister["TenMH"],
                    soTC = (int) readerRegister["SoTC"],
                    tien = (int) readerRegister["Tien"]
                };
                listRegisterClasses.Add(rc);
            }
            
        }

        public ActionResult CancelRegister(string maLopHocPhan)
        {
            string cmd = String.Format("EXEC HuyLopHocPhan '15150419', '"+ maLopHocPhan +"'");
            CommonFunction.CommonFunction.execNonQuery(cmd);
            return RedirectToAction("Index");
        }

        private void loadData()
        {
            listCourseClasses.Clear();
            SqlDataReader readerMon = CommonFunction.CommonFunction.loadData("exec LayDanhSachMonDuocDangKy '15150419',1");
            while (readerMon.Read())
            {
                string code = (string)readerMon["code"];
                if (code == "0")
                {
                    continue;
                }

                SqlDataReader readerHocPhan =
                    CommonFunction.CommonFunction.loadData("EXEC LayDanhSachHocPhanDuocDangKy '" + code + "'");
                while (readerHocPhan.Read())
                {
                    CourseClass cc = new CourseClass
                    {
                        maHocPhan = (string) readerHocPhan["code"],
                        tenHocPhan = (string) readerHocPhan["name"],
                        soTinChi = (int) readerHocPhan["numbercredit"],
                        tongSoTiet = (int) readerHocPhan["totalsession"],
                        maxStudent = (int) readerHocPhan["maxstudent"],
                        signStudent = (int) readerHocPhan["signedstudent"]
                    };
                    listCourseClasses.Add(cc);
                }
            }
        }
        
        
    }
}