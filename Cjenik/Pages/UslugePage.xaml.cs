using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cjenik
{
    /// <summary>
    /// Interaction logic for UslugePage.xaml
    /// </summary>
    public partial class UslugePage : Page
    {
        public UslugePage()
        {
            InitializeComponent();
        }

        private void PovratakBtn(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }
    }
}
