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

        /// <summary>
        /// This method recieve's html get request from the search bar on the list view and passes the first name, id, empnumber
        /// to the TeacherDataList method in TeacherData Api Controller. Parameters are optional
        /// </summary>
        /// <param name="FName"></param>
        /// <param name="id"></param>
        /// <param name="EmpNumber"></param>
        /// <returns>Returns list of teachers as NewTeacher and sends to Search view </returns>
        /// 
        //Get : Teacher/Search/{Fname}/{id}/{EmpNumber}
        //This method takes input from the html form on the list view and sends it to the searchteachers method of the teacherdatacontroller 
        //The result is sent to the search view 
        public ActionResult List(string searchKey= null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.TeacherDataList(searchKey);
            return View(Teachers);
            
        }
        //Get : Teacher/show/{id}
        //takes integer value and uses findteacher method to display additional information about a specific teacher
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        /*public ActionResult Search(string FName, int id, string EmpNumber)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> NewTeacher = controller.SearchTeachers(FName, id, EmpNumber);

            return View(NewTeacher);
        }*/

    }
}