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

namespace GameSystem
{
    /// <summary>
    /// Interaction logic for LogoutMessageWindow.xaml
    /// </summary>
    public partial class LogoutMessageWindow : Window
    {

        public int returnValue;

        public LogoutMessageWindow()
        {
            InitializeComponent();
        }


        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            returnValue = 1;
            this.Close();
        }

        private void btnCharacterSelect_Click(object sender, RoutedEventArgs e)
        {
            // 
            returnValue = 2;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            returnValue = 0;
            this.Close();
        }

    }
}
