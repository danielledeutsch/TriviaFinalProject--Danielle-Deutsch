using System;
using System.Data.SQLite;
using System.Windows;
using TriviaFinalProject.ViewModel;

namespace TriviaFinalProject.View
{
    public partial class AdminControl : Window
    {
        private const string connectionString = @"Data Source=C:\Users\danie\source\repos\TriviaFinalProject\TriviaFinalProject\TriviaProjectDB.db;Version=3;";
        private AdminUserViewModel AdminUserViewModel;
        public AdminControl()
        {
            InitializeComponent();
            AdminUserViewModel  = new AdminUserViewModel();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter the email of the user to delete.");
                return;
            }

            if (AdminUserViewModel.DeleteUser(email))
            {
                MessageBox.Show("User deleted successfully.");
            }
             else
            {
                MessageBox.Show("an error has occured.");
            }
        }


        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            string questionText = QuestionTextBox.Text.Trim();
            if (string.IsNullOrEmpty(questionText))
            {
                MessageBox.Show("Please enter the new question.");
                return;
            }

            string[] answers = new string[]
            {
                Answer1TextBox.Text.Trim(),
                Answer2TextBox.Text.Trim(),
                Answer3TextBox.Text.Trim(),
                Answer4TextBox.Text.Trim()
            };

            int correctAnswerIndex = CorrectAnswerComboBox.SelectedIndex;
            if (correctAnswerIndex == -1)
            {
                MessageBox.Show("Please select the correct answer.");
                return;
            }



            try
            {
                
                int nextQuestionId = AdminUserViewModel.GetNextQuestionId();

                AdminUserViewModel.AddQuestion(nextQuestionId, questionText);
                int currAnsId;
                    // Insert the answers
                    for (int i = 0; i < answers.Length; i++)
                    {
                    currAnsId = AdminUserViewModel.GetNextAnswerId();
                    AdminUserViewModel.AddAnswer(currAnsId, nextQuestionId, answers[i], i == correctAnswerIndex ? 1 : 0);
                       
                    }

                    StatusTextBlock.Text = "Question and answers added successfully.";
                    ClearInputs();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred78S: {ex.Message}");
            }
        }
        private void ClearInputs()
        {
            QuestionTextBox.Clear();
            Answer1TextBox.Clear();
            Answer2TextBox.Clear();
            Answer3TextBox.Clear();
            Answer4TextBox.Clear();
            CorrectAnswerComboBox.SelectedIndex = -1;
        }

       

       
    }
}