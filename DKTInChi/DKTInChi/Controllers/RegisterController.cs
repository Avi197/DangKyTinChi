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
        public List<Subject> listSubject = new List<Subject>();
        public List<Subject> registered = new List<Subject>();
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
            loadRegistered();
            return View("Registered",model: listSubject);
        }
        public ActionResult RegisterAction(int? page)
        {
            ViewModel model = new ViewModel();
            loadRegistered();
            listSubject.Clear();
            loadApprovedSubjects();

            model.list_sub = listSubject.ToList();
            model.list_registered = registered.ToList();

            return View(model: model);
        }

        [HttpPost]
        public ActionResult Register()
        {
            string id = Request.Form["id"];
            return View("RegisterAction");
        }

        /*private void loadData()
        {
            SqlDataReader reader = CommonFunction.CommonFunction.loadData("SELECT * FROM subjects order by code offset 1 row");
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