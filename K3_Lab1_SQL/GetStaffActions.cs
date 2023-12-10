using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K3_Lab1_SQL
{
    internal class GetStaffActions
    {
        public static void GetAllStaff()
        {
            Console.Clear();
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=K3_Lab1_DB;Integrated Security=True"; //connect to the DB
            using (SqlConnection conncection = new SqlConnection(connectionString))
            {
                conncection.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Personal", conncection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int personalID = reader.GetInt32(0);
                            string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                            string personalCategory = reader.GetString(reader.GetOrdinal("PersonalCategory"));
                            Console.WriteLine();
                            Console.WriteLine($"PersonalId: {personalID,-5}Name: {firstName,-10}{lastName,-15}" +
                                $"{personalCategory,-15}");
                        }
                    }
                }
            }
        }
        public static void GetStaffByCategory()  
        {
            Console.Clear();
            //use stored procedure
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=K3_Lab1_DB;Integrated Security=True"; //connect to the DB
            using (SqlConnection conncection = new SqlConnection(connectionString))
            {
                conncection.Open();
                using (SqlCommand cmd = new SqlCommand("GetStaffByCategory", conncection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                PersonalCategory: Console.WriteLine("Enter the category you want to choose: " +
                    "\n[1] Teacher" +
                    "\n[2] Substitute" +
                    "\n[3] Adminstrator" +
                    "\n[4] Principal" +
                    "\n[5] Others");
                    Console.Write("Please enter the number: ");
                    string inputNum = Console.ReadLine();
                    string input;
                    switch (inputNum)
                    {
                        case "1":
                            input = "Teacher";
                            break;
                        case "2":
                            input = "Substitute";
                            break;
                        case "3":
                            input = "Adminstrator";
                            break;
                        case "4":
                            input = "Principal";
                            break;
                        case "5":
                            input = "Others";
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid enter, please enter again! ");
                            Console.ResetColor();
                            goto PersonalCategory;
                            break;
                    }
                    cmd.Parameters.AddWithValue("@PersonalCategory", input);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int personalID = reader.GetInt32(0);
                            string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                            string personalCategory = reader.GetString(reader.GetOrdinal("PersonalCategory"));
                            Console.WriteLine();
                            Console.WriteLine($"PersonalId: {personalID, -5}Name: {firstName, -10}{lastName, -15}" +
                                $"{personalCategory}");

                        }
                    }
                }
            }
        }
    }
}
