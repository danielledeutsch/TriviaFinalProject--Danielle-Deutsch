using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
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
using TriviaFinalProject.ViewModel;

namespace TriviaFinalProject.View
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private UserViewModel UserViewModel;
        public SignUp()
        {
            InitializeComponent();
            UserViewModel = new UserViewModel();
        }


        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
            int enteredId = int.Parse(Id_box.Text);
            string enteredFirstName = FirstName_box.Text;
            string enteredEmail = Email_box.Text;
            string enteredPassword = Password_box.Text;
            string enteredLastName = LastName_box.Text;
            int enteredAdmin = 0;
           
            
                if (!UserViewModel.IsUserExist(enteredEmail, enteredPassword))
                {
                    if (UserViewModel.AddUser(enteredId, enteredFirstName, enteredLastName, enteredEmail, enteredPassword, enteredAdmin))
                    {

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please choose another password or email");
                }
            }
            catch
            {
                MessageBox.Show("Enter valid details please.");
            }
        }

            private void Back_Click(object sender, RoutedEventArgs e)
            {
                LogIn login = new LogIn();
                login.Show();
                this.Close();
            }
        }
    }



