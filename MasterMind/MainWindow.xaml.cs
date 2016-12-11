using System.Windows;
using Xceed.Wpf.Toolkit;

namespace MasterMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ustalenie początkowej lokalizacji okna aplikacji
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //uruchamianie widoku menu
            this.Content = new Menu();
        }

    }
}

