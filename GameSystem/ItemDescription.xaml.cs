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
    /// Interaction logic for ItemDescription.xaml
    /// </summary>
    public partial class ItemDescription : Window
    {
        private string itemName;
        private string itemDescription;

        public ItemDescription(string itemName, string itemDescription)
        {
            this.itemName = itemName;
            this.itemDescription = itemDescription;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblItemName.Content = itemName;
            txtblkItemDescription.Text = itemDescription;
        }
    }
}
