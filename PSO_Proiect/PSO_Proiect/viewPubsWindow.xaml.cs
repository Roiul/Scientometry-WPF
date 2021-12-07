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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PSO_Proiect
{
    /// <summary>
    /// Interaction logic for viewPubsWindow.xaml
    /// </summary>
    public partial class viewPubsWindow : UserControl
    {
        public Action backFromPubsButtonAction;
        public viewPubsWindow()
        {
            InitializeComponent();
        }

        private void backFromPubsButton_Click(object sender, RoutedEventArgs e)
        {
            this.backFromPubsButtonAction();
        }
    }
}
