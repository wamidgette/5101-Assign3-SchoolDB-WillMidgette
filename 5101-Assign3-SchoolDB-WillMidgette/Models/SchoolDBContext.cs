using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace _5101_Assign3_SchoolDB_WillMidgette.Models
{
    public class SchoolDBContext
    {
        //The following code utilized Christine Bittle's BlogProject code as a template for this solution
        //Below defined are the properties to access Christine Bittle's school database

        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //ConnectionString combines the properties above into a single string to access the database
        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }
        //This method will be used to access the database 
        /// <summary>
        /// Returns a connection to the school database.
        /// </summary>
        /// <example>
        /// private SchoolDbContext school = new SchoolDbContext();
        /// MySqlConnection Conn = school.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            //We are instantiating the MySqlConnection Class to create an object
            //the object is a specific connection to our blog database on port 3307 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }
}