using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TriviaFinalProject.ViewModel
{

    public class AdminUserViewModel
    {


        public int GetNextAnswerId()
        {
            int nextAnswerId = 1; // Default ID if table is empty
            string connectionString = @"Data Source=C:\Users\danie\source\repos\TriviaFinalProject\TriviaFinalProject\TriviaProjectDB.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT MAX(answerID) FROM HardAnswers";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        nextAnswerId = Convert.ToInt32(result) + 1;
                    }
                }
            }

            return nextAnswerId;
        }

        public int GetNextQuestionId()
        {
            int nextQuestionId = 1; // Default ID if table is empty
            string connectionString = @"Data Source=C:\Users\danie\source\repos\TriviaFinalProject\TriviaFinalProject\TriviaProjectDB.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT MAX(id) FROM HardQuestions";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        nextQuestionId = Convert.ToInt32(result) + 1;
                    }
                }
            }

            return nextQuestionId;



        }

        public static bool AddQuestion(int id, string q)
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
                            string sql = "INSERT INTO [HardQuestions] ([id], [question]) VALUES(@Id, @Question)";
                            using (var command = new SQLiteCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@Id", id);
                                command.Parameters.AddWithValue("@Question", q);
                                
                                command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            //  if an error occurs
                            transaction.Rollback();
                            MessageBox.Show($"Transaction failed 1: {ex.Message}");
                            return false; // Return false to indicate failure
                        }
                    }
                }
                return true; // Return true to indicate success
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred2: {ex.Message}");
                return false;
            }
        }

        public static bool AddAnswer(int answerId, int id, string answer, int isCorrect)
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
                            // Insert answer into HardAnswers table
                            string sql = "INSERT INTO HardAnswers (answerID, id, answer, isCorrect) VALUES (@AnswerID, @Id, @Answer, @IsCorrect)";
                            using (var command = new SQLiteCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@AnswerID", answerId);
                                command.Parameters.AddWithValue("@Id", id);
                                command.Parameters.AddWithValue("@Answer", answer);
                                command.Parameters.AddWithValue("@IsCorrect", isCorrect);
                                command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction if an error occurs
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

        public static bool DeleteUser(string email)
        {
            string connectionString = @"Data Source=C:\Users\danie\source\repos\TriviaFinalProject\TriviaFinalProject\TriviaProjectDB.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM Users WHERE Email = @Email";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }
    }

}
