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
        /// This method recieve's html get request from the search bar on the list view and passes the searchKey
        /// to the TeacherDataList method in TeacherData Api Controller. Parameters are optional
        /// </summary>
        /// <param name="FName"></param>
        /// <param name="id"></param>
        /// <param name="EmpNumber"></param>
        /// <returns>Returns list of teachers as NewTeacher and sends to Search view </returns>
        /// 
        //Get : Teacher/List/{searchKey?}
        //This method takes input from the html form on the list view and sends it to the searchteachers method of the teacherdatacontroller 
        //The result is sent to the search view 
        public ActionResult List(string searchKey = null)
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
        /// <summary>
        /// calls the findteacher method and returns the appropriate teacher. Sends model to view Confirm Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ConfirmDelete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }
        [HttpPost]
        public ActionResult DeleteTeacher(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("Deleted");
        }
        public ActionResult Deleted()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// Add view sends parameters from input fields. No parameters are nullable. 
        /// Information is sent to the AddTeacher method in the API controller. 
        /// hireDate is converted to a string before sent to API controller
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="empNum"></param>
        /// <param name="hireDate"></param>
        /// <param name="salary"></param>
        /// <returns>After teacher has been added to database, redirects to the TeacherAdded view, lettign the user know the info was input sucessfully</returns>
        [HttpPost]
        public ActionResult AddTeacher(string fName, string lName, string empNum, DateTime hireDate, decimal salary)
        {
            
            TeacherDataController controller = new TeacherDataController();
            var hireDateString = hireDate.ToString("yyyy-MM-dd");
            controller.AddTeacher(fName, lName, empNum, hireDateString, salary);
            return RedirectToAction("TeacherAdded");
        }
       /// <summary>
       /// Lets user know the teacher was entered sucessfuly 
       /// </summary>
       /// <returns>view TeacherAdded </returns>
        public ActionResult TeacherAdded()
        {
            return View();
        }

    }
}