using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using _5101_Assign3_SchoolDB_WillMidgette.Models;
using MySql.Data.MySqlClient;


namespace _5101_Assign3_SchoolDB_WillMidgette.Controllers
{
    public class TeacherDataController : ApiController
    {
        //creates instance of SchoolDBContext class called "school"
        private SchoolDBContext school = new SchoolDBContext();

        [HttpGet]
        public IEnumerable<Teacher> TeacherDataList()
        {
            //create instance of MySqlConnection "Conn" and call the AccessDatabase
            //method to access the school database 
            MySqlConnection Conn = school.AccessDatabase();

            //open the connection to database
            Conn.Open();

            //create command or query to mysql database 
            MySqlCommand cmd = Conn.CreateCommand();

            //Tell mysql what data to return from Teachers 
            cmd.CommandText = "Select * FROM teachers";

            //MySql returns data in table format - must be read thru 
            //datareader for web api to interpret
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //new list of Teacher class called Teachers 
            List<Teacher> Teachers = new List<Teacher>{};

            //read return of query line by line. While there are lines left
            //to read, run the following code 
            while (ResultSet.Read())
            {

                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                //adds this iteration's NewTeacher to the list of teachers 
                Teachers.Add(NewTeacher);
            }

            //close the connection to school database 
            Conn.Close();

            return Teachers;
        }
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //create instance of MySqlConnection "Conn" and call the AccessDatabase
            //method to access the school database 
            MySqlConnection Conn = school.AccessDatabase();

            //open the connection to database
            Conn.Open();

            //create command or query to mysql database 
            MySqlCommand cmd = Conn.CreateCommand();

            //Tell mysql what data to return from Teachers - I want all of it
            cmd.CommandText = "Select * FROM teachers where teacherid = "+id;

            //MySql returns data in table format - must be read thru 
            //datareader for web api to interpret
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read()) 
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
            }

            return NewTeacher;
        }
        /// <summary>
        /// Recieves Fname, id, and empnumber thru http get request, 
        /// </summary>
        /// <param name="FName"></param>
        /// <param name="id"></param>
        /// <param name="EmpNumber"></param>
        /// <returns> returns a list of teachers matching search</returns>
        [HttpGet]
        [Route("api/teacherdata/searchteachers/{Name}/{id}/{EmpNumber}")]
        //C# did not allow me to make string inputs nullable so the user must input all 3 variables to recieve a search result 
        public IEnumerable<Teacher> SearchTeachers (string FName, int id, string EmpNumber)
        {

            MySqlConnection Conn = school.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //Inputs the arguments of this method into the MySql query 
            cmd.CommandText = "Select * from teachers WHERE (teacherfname LIKE '%" + FName + "%' AND teacherid LIKE '%" + id + "%' AND employeenumber LIKE '%" + EmpNumber + "%')";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teachers = new List<Teacher> { };

            //I wrote this as a loop because ideally, a user could search by only 1 of the 3 arguments (name or id or empnumber)
            //in this case, there would be multiple teacher results from the database 
            while (ResultSet.Read())
            {

                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                //adds this iteration's NewTeacher to the list of teachers 
                Teachers.Add(NewTeacher);
            }

            return Teachers;
        }

    }
}
