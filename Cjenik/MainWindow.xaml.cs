using System.Windows;

namespace Cjenik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UslugeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new UslugePage();

        }
        private void TipUslugeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KlijentiBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new KlijentiPage();
            
        }
    }
}
