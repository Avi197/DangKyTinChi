using System.Web.Mvc;
using System.Data.SqlClient;
using DKTInChi.Models;
using System.Collections.Generic;

namespace DKTInChi.Controllers
{
    public class RegisterController : Controller
    {

        public List<Subject> listSubject = new List<Subject>();
        public List<Subject> listRegister = new List<Subject>();
        
        public List<CourseClass> listCourseClasses = new List<CourseClass>();

        // GET
        public ActionResult Index()
        {
            loadData();
            ViewData["ListRegister"] = listRegister;
            return View(model: listCourseClasses);
        }

        [HttpGet]
        public ActionResult RegisterAction(string code)
        {
            Subject sb = listSubject.Find(s => s.code == code);
            listRegister.Add(sb);
            ViewData["ListRegister"] = listRegister;
            return RedirectToAction("Index");
        }

        private void registerAction()
        {
            string cmd = "";
        }

        private void loadData()
        {
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