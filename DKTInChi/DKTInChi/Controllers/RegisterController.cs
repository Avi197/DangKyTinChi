using System;
using System.Web.Mvc;
using System.Data.SqlClient;
using DKTInChi.Models;
using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace DKTInChi.Controllers
{
    public class RegisterController : Controller
    {
<<<<<<< HEAD

        
        public List<CourseClass> listCourseClasses = new List<CourseClass>();
        public List<RegisterClass> listRegisterClasses = new List<RegisterClass>();

=======
        public List<Subject> listSubject = new List<Subject>();
        public List<Subject> registered = new List<Subject>();
>>>>>>> origin/trung-etc
        // GET
        public ActionResult approvedRegister(int? page)
        {
            ViewModel model = new ViewModel();
            loadApprovedSubjects();
            model.subjects = listSubject.ToList().ToPagedList(page ?? 1, 10);
            return View(model: model);
        }
        public ActionResult Registered()
        {
<<<<<<< HEAD
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
            
=======
            loadRegistered();
            return View("Registered",model: listSubject);
>>>>>>> origin/trung-etc
        }
        public ActionResult RegisterAction(int? page)
        {
            ViewModel model = new ViewModel();
            loadRegistered();
            listSubject.Clear();
            loadApprovedSubjects();

            model.list_sub = listSubject.ToList();
            model.list_registered = registered.ToList();

<<<<<<< HEAD
        public ActionResult CancelRegister(string maLopHocPhan)
        {
            string cmd = String.Format("EXEC HuyLopHocPhan '15150419', '"+ maLopHocPhan +"'");
            CommonFunction.CommonFunction.execNonQuery(cmd);
            return RedirectToAction("Index");
=======
            return View(model: model);
        }

        [HttpPost]
        public ActionResult Register()
        {
            string id = Request.Form["id"];
            return View("RegisterAction");
>>>>>>> origin/trung-etc
        }

        /*private void loadData()
        {
<<<<<<< HEAD
            listCourseClasses.Clear();
            SqlDataReader readerMon = CommonFunction.CommonFunction.loadData("exec LayDanhSachMonDuocDangKy '15150419',1");
            while (readerMon.Read())
=======
            SqlDataReader reader = CommonFunction.CommonFunction.loadData("SELECT * FROM subjects order by code offset 1 row");
            while (reader.Read())
>>>>>>> origin/trung-etc
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
        }*/

        /* UNDER CONSTRUCTION
        private void deleteRegisteredSubject()
        {
            SqlDataReader reader = CommonFunction.CommonFunction.loadData("delete from register " + "'" + Session["username"]);            
        }*/
        private void loadRegistered()
        {
            SqlDataReader reader = CommonFunction.CommonFunction.loadData("select B.subjectcode,E.name,E.numbercredit,D.code as room from register A"
                                                                           + " inner join course B on B.code = A.courseclasscode"
                                                                           + " inner join courseschedule C on C.coursecode = B.code"
                                                                           + " inner join room D on D.code = C.roomcode"
                                                                           + " inner join subjects E on E.code = B.subjectcode"
                                                                           + " where A.studentcode = " + "'" + Session["username"] + "'");
            while (reader.Read())
            {
                string code = (string)reader["subjectcode"];
                string name = (string)reader["name"];
                int numbercredit = (int)reader["numbercredit"];
                string place = (string)reader["room"];

                Subject subject = new Subject
                {
                    code = code,
                    name = name,
                    numbercredit = numbercredit,
                    place = place
                };
                listSubject.Add(subject);
                registered.Add(subject);
            }
        }
        private void loadApprovedSubjects()
        {
            SqlDataReader reader = CommonFunction.CommonFunction.loadData("exec LayDanhSachMonDuocDangKy " + "'" + Session["username"] + "'" + ",'2'");
            while (reader.Read())
            {
                string code = (string)reader["subject_ID"];
                string name = (string)reader["name"];
                int numbercredit = (int)reader["numbercredit"];
                int theory = (int)reader["theory"];
                int task = (int)reader["task"];
                int practice = (int)reader["pratice"];
                int discussion = (int)reader["discussion"];
                int totalsession = (int)reader["totalsession"];

                Subject subject = new Subject
                {
                    code = code,
                    name = name,
                    numbercredit = numbercredit,
                    theory = theory,
                    task = task,
                    pratice = practice,
                    discussion = discussion,
                    totalsession = totalsession
                };
                listSubject.Add(subject);
            }
        }
        
        
    }
}