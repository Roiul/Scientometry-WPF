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

namespace PSO_Proiect
{
    /// <summary>
    /// Interaction logic for pubDetailsWindow.xaml
    /// </summary>
    public partial class pubDetailsWindow : Window
    {
        public appDBDataContext appDB = new appDBDataContext();
        public pubDetailsWindow()
        {
            InitializeComponent();

            var types=(from type in appDB.Tip_Publicaties
                       select type.Tip).ToList();
            this.typeComboBox.ItemsSource = types;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
