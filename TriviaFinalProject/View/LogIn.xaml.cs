using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriviaFinalProject.ViewModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace TriviaFinalProject.View
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        private UserViewModel UserViewModel;
        public LogIn()
        {
            InitializeComponent();
            UserViewModel = new UserViewModel();
        }

        private void LogIn_Button_Click(object sender, RoutedEventArgs e)
        {
            
            string enteredEmail = Email_Box.Text;
            string enteredPassword = Password_Box.Text;
            if (UserViewModel.IsUserExist(enteredEmail, enteredPassword))
            {
                string name = UserViewModel.GetName(enteredEmail, enteredPassword);
                if (UserViewModel.IsAdmin(enteredEmail, enteredPassword) == 1)
                {
                    // Admin found, create and show the AdminControl window
                    AdminControl adminControl = new AdminControl();
                    adminControl.Show();
                    this.Close();
                }
                else
                {
                    // Regular user found, create and show the GameMenu window
                    GameMenu gamemenu = new GameMenu(name);
                    gamemenu.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid email or password. Please try again.");
            }

        }

        private void SignUp_Button_Click(object sender, RoutedEventArgs e)
        {
            SignUp signup = new SignUp();
            signup.Show();
        }
    }
}
