using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaFinalProject.Model;
using System.Data.SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;

namespace TriviaFinalProject.ViewModel
{
    public class UserViewModel
    {
       public string GetName(string email, string password)
        {
            string rslt = string.Empty;
            string connectionString = @"Data Source=C:\Users\danie\source\repos\TriviaFinalProject\TriviaFinalProject\TriviaProjectDB.db;Version=3;";
            using (var connection = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                connection.Open();

                // search for user
                string sql = "SELECT first_name FROM Users WHERE Email = @Email AND Password = @Password LIMIT 1";
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // If a user is found
                        {
                            rslt= reader.GetString(0);

                        }
                    }
                }
            }
            return rslt;
        }

        public bool IsUserExist(string enteredEmail, string enteredPassword)
        {
            string connectionString = @"Data Source=C:\Users\danie\source\repos\TriviaFinalProject\TriviaFinalProject\TriviaProjectDB.db;Version=3;";
            bool userFound = false; 


            using (var connection = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                connection.Open();

                // search for user
                string sql = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password LIMIT 1";
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", enteredEmail);
                    command.Parameters.AddWithValue("@Password", enteredPassword);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // If a user is found
                        {
                            userFound = true; 

                        }
                    }
                }
            }
            return userFound;
        }

        public int IsAdmin(string enteredEmail, string enteredPassword)
        {
            string connectionString = @"Data Source=C:\Users\danie\source\repos\TriviaFinalProject\TriviaFinalProject\TriviaProjectDB.db;Version=3;";
            int isAdmin = 0; // Flag to indicate if user is admin

            using (var connection = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                connection.Open();

                // SQL command to search for user
                string sql = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password LIMIT 1";
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", enteredEmail);
                    command.Parameters.AddWithValue("@Password", enteredPassword);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // If a user is found
                        {
                            isAdmin = Convert.ToInt32(reader["IsAdmin"]); // Check if user is admin
                        }
                    }
                }
            }
            return isAdmin;
        }
        public bool AddUser(int id, string firstName, string lastName, string email, string password, int isAdmin)
        {
            try
            {
                string connectionString = @"Data Source=C:\Users\danie\source\repos\TriviaFinalProject\TriviaFinalProject\TriviaProjectDB.db;Version=3;";
                using (var connection = new System.Data.SQLite.SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // insert user into Users table
                            string sql = "INSERT INTO [Users] ([Id], [first_name], [last_name], [email], [password], [IsAdmin]) VALUES(@Id, @FirstName, @LastName, @Email, @Password, @IsAdmin)";
                            using (var command = new SQLiteCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@Id", id);
                                command.Parameters.AddWithValue("@FirstName", firstName);
                                command.Parameters.AddWithValue("@LastName", lastName);
                                command.Parameters.AddWithValue("@Email", email);
                                command.Parameters.AddWithValue("@Password", password);
                                command.Parameters.AddWithValue("@IsAdmin", isAdmin);
                                command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            //  if an error occurs
                            transaction.Rollback();
                            MessageBox.Show($"Transaction failed: {ex.Message}");
                            return false; // Return false to indicate failure
                        }
                    }
                }
                return true; // Return true to indicate success
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }


    }
}

      
    

