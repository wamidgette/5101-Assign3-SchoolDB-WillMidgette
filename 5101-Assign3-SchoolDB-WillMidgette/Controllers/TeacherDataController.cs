using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using _5101_Assign3_SchoolDB_WillMidgette.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace _5101_Assign3_SchoolDB_WillMidgette.Controllers
{
    public class TeacherDataController : ApiController
    {
        //creates instance of SchoolDBContext class called "school"
        private SchoolDBContext school = new SchoolDBContext();

        [HttpGet]
        [Route("api/teacherdata/teacherdatalist/{searchKey?}")]
        public IEnumerable<Teacher> TeacherDataList(string searchkey =null)
        {
            //create instance of MySqlConnection "Conn" and call the AccessDatabase
            //method to access the school database 
            MySqlConnection Conn = school.AccessDatabase();

            //open the connection to database
            Conn.Open();

            //create command or query to mysql database 
            MySqlCommand cmd = Conn.CreateCommand();
            //Tell mysql what data to return from Teachers 
            cmd.CommandText = "Select * from teachers WHERE (teacherfname LIKE @key OR teacherid LIKE  @key  OR teacherlname LIKE @key)";
            //paramatize 
            cmd.Parameters.AddWithValue("@key","%" + searchkey + "%");
            cmd.Prepare();
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

            Conn.Close();

            return NewTeacher;
        }
        /// <summary>
        /// method deletes teacher from database using the teacherid id in the query 
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //create instance of MySqlConnection "Conn" and call the AccessDatabase
            //method to access the school database 
            MySqlConnection Conn = school.AccessDatabase();

            //open the connection to database
            Conn.Open();

            //create command or query to mysql database 
            MySqlCommand cmd = Conn.CreateCommand();

            //Delete command
            cmd.CommandText = "DELETE FROM teachers where teacherid=@id";
            //Sanitize 
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Conn.Close();
        }
        /// <summary>
        /// Adds teacher to the database from the parameters taken from user fields 
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="empNum"></param>
        /// <param name="hireDateString"></param>
        /// <param name="salary"></param>
        [HttpPost]
        [Route("api/teacherdata/AddTeacher/{fName}/{lName}/{empNum}/{hireDate}")]
        public void AddTeacher(string fName, string lName, string empNum, string hireDateString, decimal salary)
        {
            MySqlConnection Conn = school.AccessDatabase();

            Conn.Open();

            MySqlCommand Cmd = Conn.CreateCommand();
            //Create mysql command 
            Cmd.CommandText = "Insert into teachers(teacherfname, teacherlname, employeenumber, hiredate, salary) Values(@firstName,@lastName,@employeeNumber,@hireDate, @salary)";
            //sanitize 
            Cmd.Parameters.AddWithValue("@firstName", fName);
            Cmd.Parameters.AddWithValue("@lastName", lName);
            Cmd.Parameters.AddWithValue("@employeeNumber", empNum);
            Cmd.Parameters.AddWithValue("@hireDate", hireDateString);
            Cmd.Parameters.AddWithValue("@salary", salary);
            Cmd.Prepare();
            //Debug.WriteLine(Cmd.CommandText);
            Cmd.ExecuteNonQuery();

            Conn.Close();
        }

    }
}
