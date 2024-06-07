using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TriviaFinalProject.ViewModel;

namespace TriviaFinalProject.View
{
    /// <summary>
    /// Interaction logic for HardGame.xaml
    /// </summary>
    public partial class HardGame : Window
    {
        private const string connectionString = @"Data Source=C:\Users\danie\source\repos\TriviaFinalProject\TriviaFinalProject\TriviaProjectDB.db;Version=3;";


        public HardGame()
        {
           
            InitializeComponent();
            answerButtons = new List<Button> { AnswerButton1, AnswerButton2, AnswerButton3, AnswerButton4 };
            DisplayQWithNum(currentQNum);
        }
         private int currentQNum = 1;
        private int totalQuestions = GetNumQuestions();
        private List<Button> answerButtons;

        private static int GetNumQuestions()
        {
            int nextQuestionId = 1; // Default ID if table is empty

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT MAX(id) FROM HardQuestions";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        nextQuestionId = Convert.ToInt32(result) ;
                    }
                }
            }

            return nextQuestionId;
        }
    
    private void DisplayQWithNum(int num)
        {
            correctSound.Stop();
            string connectionString = @"Data Source=C:\Users\danie\source\repos\TriviaFinalProject\TriviaFinalProject\TriviaProjectDB.db;Version=3;";
            string question = string.Empty;
            List<(string answer, bool isCorrect)> answers = new List<(string, bool)>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Retrieve the question
                string questionSql = "SELECT question FROM HardQuestions WHERE id = @Id";
                using (var questionCommand = new SQLiteCommand(questionSql, connection))
                {
                    questionCommand.Parameters.AddWithValue("@Id", num);
                    using (var reader = questionCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            question = reader["question"].ToString();
                        }
                    }
                }

                // Retrieve the answers
                string answersSql = "SELECT answer, isCorrect FROM HardAnswers WHERE id = @Id";
                using (var answersCommand = new SQLiteCommand(answersSql, connection))
                {
                    answersCommand.Parameters.AddWithValue("@Id", num);
                    using (var reader = answersCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string answer = reader["answer"].ToString();
                            bool isCorrect = reader.GetInt32(reader.GetOrdinal("isCorrect")) == 1;
                            answers.Add((answer, isCorrect));
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(question) && answers.Count == 4)
            {
                QuestionTextBlock.Text = question;
                for (int i = 0; i < 4; i++)
                {
                    answerButtons[i].Content = answers[i].answer;
                    answerButtons[i].Tag = answers[i].isCorrect;
                    answerButtons[i].Background = Brushes.LightGray;
                }
                ResultTextBlock.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("youve finished the trivia! good job.");
                // Optionally, disable the "Continue" button or close the window
            }
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            bool isCorrect = (bool)clickedButton.Tag;

            foreach (var button in answerButtons)
            {
                if ((bool)button.Tag)
                {
                    button.Background = Brushes.LightGreen;
                }
                else
                {
                    button.Background = Brushes.Red;
                }
            }

            if (isCorrect )
            {
                ResultTextBlock.Text = "Correct!";
                correctSound.Play();
                
            }
            else
            {
                ResultTextBlock.Text = "Incorrect";
            }
        }



        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            currentQNum++;
            if (currentQNum <= totalQuestions)
            {
                DisplayQWithNum(currentQNum);
            }
            else
            {
                MessageBox.Show("You have reached the end of the questions.");
                
            }
        }
    }
}
