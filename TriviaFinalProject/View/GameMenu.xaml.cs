using System;
using System.Collections.Generic;
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

namespace TriviaFinalProject.View
{
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : Window
    {
        public GameMenu(string name)
        {
            InitializeComponent();
            Welcome_Block.Text = "Hey, " + name + "!";
        }

        
        private void play_click(object sender, RoutedEventArgs e)
        {
            HardGame hardgame = new HardGame();
            hardgame.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("a question will appear on screen along with 4 options, click one of the answeres you think is correct. the correct one will turn green and the rest red. when ready for the next question press the 'continue' button.");
        }
    }
}
