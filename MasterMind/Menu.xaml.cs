using System.Windows;
using System.Windows.Controls;

namespace MasterMind
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            this.Content = new Gameboard();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void Button_Click_Instruction(object sender, RoutedEventArgs e)
        {
            this.Content = new Instruction();
        }
    }
}
