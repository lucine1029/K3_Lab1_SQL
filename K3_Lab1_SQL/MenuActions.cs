using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace K3_Lab1_SQL
{
    internal class MenuActions
    {
        public static void MainMenu()
        {
        MainMenuList: Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("******************Main Menu********************" +
                "\n[1] Get all students" +
                "\n[2] Get all students in a certain class" +
                "\n[3] Add new staff" +
                "\n[4] Get staff" +
                "\n[5] Get all grades set in the last month" +
                "\n[6] Get average grade per course" +
                "\n[7] Get average grade by gender and by age year" +
                "\n[8] Add new students" +
                "\n[0] Exit the program" +
                "\n***********************************************" +
                "\nEnter your option:");
            Console.ResetColor();
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    MenuActions.GetAllStudents();
                    goto MainMenuList;
                    break;
                case "2":
                    MenuActions.GetStudentsByClass();
                    goto MainMenuList;
                    break;
                case "3":
                    MenuActions.AddNewStaff();
                    goto MainMenuList;
                    break;
                case "4":
                    MenuActions.GetStaff();
                    goto MainMenuList;
                    break;
                case "5":
                    MenuActions.GetAllGradesFromLastMonth();
                    goto MainMenuList;
                    break;
                case "6":
                    MenuActions.GetAverageGrade();
                    goto MainMenuList;
                    break;
                case "7":
                    MenuActions.GetAverageGradesByGenderByAge();
                    goto MainMenuList;
                    break;
                case "8":
                    MenuActions.AddNewStudent();
                    goto MainMenuList;
                    break;
                case "0":
                    MenuActions.ExitProgram();
                    break;
                default:
                    Console.WriteLine("Invalid enter, please enter again! ");
                    goto MainMenuList;
                    break;
            }
        }
        public static void GetAllStudents()
        {
            //The user can choose whether they want to see the students sorted by first or last name and whether it should be sorted in ascending or descending order.
            Console.Clear();
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=K3_Lab1_DB;Integrated Security=True"; //connect to the DB
            using (SqlConnection conncection = new SqlConnection(connectionString))
            {
                conncection.Open();
            OrderByCategory: Console.Write("\nSort by first name [F] or lastName [L]? Please enter: ");
                string OrderByCategory = Console.ReadLine().ToUpper();
                string OrderByC;
                switch (OrderByCategory)
                {
                    case "F":
                        OrderByC = "firstName";
                        break;
                    case "L":
                        OrderByC = "lastName";
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid enter, please enter again! ");
                        Console.ResetColor();
                        goto OrderByCategory;
                        break;
                }
            OrderByAscOrDesc: Console.Write("\nSort by asceding [A] or descending [D]? Please enter: ");
                string Order = Console.ReadLine().ToUpper();
                string OrderByA_D;
                switch (Order)
                {
                    case "A":
                        OrderByA_D = "ASC";
                        break;
                    case "D":
                        OrderByA_D = "DESC";
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid enter, please enter again! ");
                        Console.ResetColor();
                        goto OrderByAscOrDesc;
                        break;
                }
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Students ORDER BY {OrderByC} {OrderByA_D}", conncection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                            int ssn = reader.GetInt32(reader.GetOrdinal("SSN"));
                            DateTime dateOfBirth = (DateTime)reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                            char gender = Convert.ToChar(reader["Gender"]);
                            int classID = reader.GetInt32(reader.GetOrdinal("ClassID_FK"));
                            Console.WriteLine();
                            Console.WriteLine($"StudentId: {id,-5}Name: {firstName,-10}{lastName,-15}SSN: {ssn,-10}" +
                                $"DOB:{dateOfBirth.ToShortDateString(),-15}{gender,-5}ClassID: {classID}");
                        }
                    }
                }
            }
            Console.WriteLine("\nClick [Enter] to go back to the main menu.");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);
        }
        public static void GetStudentsByClass()  
        {
            //The user must first see a list of all classes that exist, then the user can select one of the classes and then all the students in that class will be printed.
            //Extra challenge: let the user also choose the sorting of the students as in the point above.
            Console.Clear();
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=K3_Lab1_DB;Integrated Security=True"; //connect to the DB
            using (SqlConnection conncection = new SqlConnection(connectionString))
            {
                conncection.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Classes ", conncection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int classID = reader.GetInt32(0);
                            string className = reader.GetString(reader.GetOrdinal("ClassName"));
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"ClassId: {classID,-10}ClassName: {className,-15}");
                            Console.WriteLine();
                            Console.ResetColor();
                        }
                    }
                }

                Console.Write("Enter [ClassId] to get all the student for the class: ");
                string input = Console.ReadLine();
            //Extra challenge: let the user also choose the sorting of the students as in the point above.
            OrderByCategory: Console.Write("\nSort by first name [F] or lastName [L]? Please enter: "); 
                string OrderByCategory = Console.ReadLine().ToUpper();
                string OrderByC;
                switch (OrderByCategory)
                {
                    case "F":
                        OrderByC = "firstName";
                        break;
                    case "L":
                        OrderByC = "lastName";
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid enter, please enter again! ");
                        Console.ResetColor();
                        goto OrderByCategory;
                        break;
                }
            OrderByAscOrDesc: Console.Write("\nSort by asceding [A] or descending [D]? Please enter: ");
                string Order = Console.ReadLine().ToUpper();
                string OrderByA_D;
                switch (Order)
                {
                    case "A":
                        OrderByA_D = "ASC";
                        break;
                    case "D":
                        OrderByA_D = "DESC";
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid enter, please enter again! ");
                        Console.ResetColor();
                        goto OrderByAscOrDesc;
                        break;
                }
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Students WHERE ClassID_FK = @ClassID_FK ORDER BY {OrderByC} {OrderByA_D}", conncection))
                {
                    cmd.Parameters.AddWithValue("@ClassID_FK", input);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                            int ssn = reader.GetInt32(reader.GetOrdinal("SSN"));
                            DateTime dateOfBirth = (DateTime)reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                            char gender = Convert.ToChar(reader["Gender"]);
                            int classID = reader.GetInt32(reader.GetOrdinal("ClassID_FK"));
                            Console.WriteLine();
                            Console.WriteLine($"StudentId: {id,-5}{firstName,-10}{lastName,-15}SSN: {ssn,-10}" +
                                $"{dateOfBirth.ToShortDateString(),-15}{gender,-5}ClassID: {classID}");
                        }
                    }
                }
                Console.WriteLine("\nClick [Enter] to go back to the main menu.");
                ConsoleKeyInfo key;
                do
                {
                    key = Console.ReadKey(true);
                }
                while (key.Key != ConsoleKey.Enter);                
            }
        }
        public static void AddNewStaff()
        {
            //The user must be able to enter information about a new employee and that data is then saved in the database.
            Console.Clear();
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=K3_Lab1_DB;Integrated Security=True"; //connect to the DB
            using (SqlConnection conncection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Personal(FirstName, LastName, PersonalCategory) " +
                    "VALUES(@FirstName, @LastName, @PersonalCategory)", conncection))
                {
                    Console.WriteLine("Welcome to add an new staff, please enter the following infomation:");
                    Console.WriteLine("Enter the Fisrt Name:");
                    string inputFirstName = Console.ReadLine();
                    cmd.Parameters.AddWithValue("@FirstName", inputFirstName);
                    Console.WriteLine("Enter the Last Name:");
                    string inputLastName = Console.ReadLine();
                    cmd.Parameters.AddWithValue("@LastName", inputLastName);
                    Console.WriteLine("Enter the Personal Category:");
                    string inputCategory = Console.ReadLine();
                    cmd.Parameters.AddWithValue("@PersonalCategory", inputCategory);
                    conncection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rows} row inserted.");
                }
            }
            Console.WriteLine("\nClick [Enter] to go back to the main menu.");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);
        }
        public static void GetStaff()
        {
            //The user can choose whether he wants to see all employees, or only within one of the categories, such as teachers.
            Console.Clear();
        GetStaffMenu: Console.Write("Please choose functions below:" +
            "\n [1] Get all staff" +
            "\n [2] Get staff by category");
            Console.Write("\nPlease enter [1] or [2]: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    GetStaffActions.GetAllStaff();
                    break;
                case "2":
                    GetStaffActions.GetStaffByCategory();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid enter, please enter again! ");
                    Console.ResetColor();
                    goto GetStaffMenu;
                    break;
            }
            Console.WriteLine("\nClick [Enter] to go back to the main menu.");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);
        }
        public static void GetAllGradesFromLastMonth()
        {
            //Here, the user can see a list of all the grades set in the last month, where the student's name,
            //the name of the course and the grade appear.
            Console.Clear();
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=K3_Lab1_DB;Integrated Security=True"; //connect to the DB
            using (SqlConnection conncection = new SqlConnection(connectionString))
            {
                conncection.Open();
                using (SqlCommand cmd = new SqlCommand("GetAllGradesFromLastMonth", conncection)) //use stored procedure
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                            string courseName = reader.GetString(reader.GetOrdinal("CourseName"));
                            int grade = reader.GetInt32(reader.GetOrdinal("Grade"));
                            DateTime gradeDate = (DateTime)reader.GetDateTime(reader.GetOrdinal("GradeSetInDate"));
                            Console.WriteLine($"Name: {firstName,-10}{lastName,-15}" +
                                $"Course: {courseName,-25}Grade: {grade,-5}Grade Date: {gradeDate.ToShortDateString(),-15}");
                            Console.WriteLine();
                        }
                    }
                }
            }
            Console.WriteLine("\nClick [Enter] to go back to the main menu.");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);
        }
        public static void GetAverageGrade()
        {
            //Get a list of all courses and the average grade that students received in that course,
            //as well as the highest and lowest grade that someone received in the course.
            //use stored procedure
            Console.Clear();
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=K3_Lab1_DB;Integrated Security=True"; //connect to the DB
            using (SqlConnection conncection = new SqlConnection(connectionString))
            {
                conncection.Open();
                using (SqlCommand cmd = new SqlCommand("GetAverageGrade", conncection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int courseID = reader.GetInt32(reader.GetOrdinal("CourseID"));
                            string courseName = reader.GetString(reader.GetOrdinal("CourseName"));
                            int averageGrade = reader.GetInt32(reader.GetOrdinal("AverageGrade"));
                            int highestGrade = reader.GetInt32(reader.GetOrdinal("HighestGrade"));
                            int lowestGrade = reader.GetInt32(reader.GetOrdinal("LowestGrade"));
                            Console.WriteLine($"CourseID: {courseID,-5}Course: {courseName,-25}" +
                                $"AverageGrade: {averageGrade,-5}Highest: {highestGrade,-5}Lowest: {lowestGrade}");
                            Console.WriteLine();
                        }
                    }
                }
            }
            Console.WriteLine("\nClick [Enter] to go back to the main menu.");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);
        }
        public static void AddNewStudent()
        {
            //The user gets the opportunity to enter information about a new student and that data is then saved in the database.
            Console.Clear();
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=K3_Lab1_DB;Integrated Security=True"; //connect to the DB
            using (SqlConnection conncection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Students(FirstName, LastName, SSN, DateOfBirth, Gender, ClassId_FK) " +
                    "VALUES(@FirstName, @LastName, @SSN, @DateOfBirth, @Gender, @ClassID_FK)", conncection))
                {
                    Console.WriteLine("Welcome to add an new student, please enter the following infomation:");
                    Console.WriteLine("Enter the Fisrt Name:");
                    string inputFirstName = Console.ReadLine();
                    cmd.Parameters.AddWithValue("@FirstName", inputFirstName);
                    Console.WriteLine("Enter the Last Name:");
                    string inputLastName = Console.ReadLine();
                    cmd.Parameters.AddWithValue("@LastName", inputLastName);
                    Console.WriteLine("Enter the Social Security Number(SSN):");
                    string inputSSN = Console.ReadLine();
                    cmd.Parameters.AddWithValue("@SSN", inputSSN);
                    Console.WriteLine("Enter the DateOfBirth(YYYY-MM-DD):");
                    string inputDateOfBirth = Console.ReadLine();
                    cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(inputDateOfBirth));
                    Console.WriteLine("Enter the Gender(M or F):");
                    string inputGender = Console.ReadLine().ToUpper();
                    cmd.Parameters.AddWithValue("@Gender", Convert.ToChar(inputGender));
                    Console.WriteLine("Enter the ClassID:");
                    string inputClassID = Console.ReadLine();
                    cmd.Parameters.AddWithValue("@ClassID_FK", Convert.ToInt32(inputClassID));
                    conncection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{rows} row inserted.");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("\nClick [Enter] to go back to the main menu.");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);
        }
        public static void ExitProgram()
        {
            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        public static void GetAverageGradesByGenderByAge()  //Extra
        {
            //Extra: Build another function for the user where it is possible to obtain the average grade based partly on gender
            //and partly on age group/year group, in terms of the average for all courses they have taken

            Console.Clear();
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=K3_Lab1_DB;Integrated Security=True"; //connect to the DB
            using (SqlConnection conncection = new SqlConnection(connectionString))
            {
                conncection.Open();
                using (SqlCommand cmd = new SqlCommand("GetAverageGradesByGenderByAge", conncection)) //use stored procedure
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int courseID = reader.GetInt32(reader.GetOrdinal("CourseID"));
                            string courseName = reader.GetString(reader.GetOrdinal("CourseName"));
                            int averageGrade = reader.GetInt32(reader.GetOrdinal("AverageGrade"));
                            int ageYear = reader.GetInt32(reader.GetOrdinal("Year"));
                            char gender = Convert.ToChar(reader["Gender"]);
                            Console.WriteLine($"CourseID: {courseID, -5}Course: {courseName,-25}" +
                                $"Average Grade: {averageGrade,-5}{gender,-5}age_year: {ageYear,-15}");
                            Console.WriteLine();
                        }
                    }
                }
            }
            Console.WriteLine("\nClick [Enter] to go back to the main menu.");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);
        }
    }

        //Extra challenges
        //Check that the social security numbers are valid through SQL.---- Havn't done, need to come back
        
    
}
