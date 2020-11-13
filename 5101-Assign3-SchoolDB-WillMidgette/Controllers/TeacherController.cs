using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5101_Assign3_SchoolDB_WillMidgette.Models;

namespace _5101_Assign3_SchoolDB_WillMidgette.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/list
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.TeacherDataList();
            return View(Teachers);
        }
        //Get : Teacher/show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            
            return View(NewTeacher);
        }
        /// <summary>
        /// This method recieve's html get request from the search bar on the list view and passes the first name, id, empnumber
        /// to the SearchTeacher method in TeacherData Api Controller
        /// </summary>
        /// <param name="FName"></param>
        /// <param name="id"></param>
        /// <param name="EmpNumber"></param>
        /// <returns>Returns list of teachers as NewTeacher and sends to Search view </returns>
        /// 
        //Get : Teacher/Search/{Fname}/{id}/{EmpNumber}
        public ActionResult Search(string FName, int id, string EmpNumber)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> NewTeacher = controller.SearchTeachers(FName, id, EmpNumber);

            return View(NewTeacher);
        }

    }
}