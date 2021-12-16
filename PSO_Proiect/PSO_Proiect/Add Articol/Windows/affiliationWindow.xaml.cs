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
    /// Interaction logic for affiliationWindow.xaml
    /// </summary>
    public partial class affiliationWindow : Window
    {
        appDBDataContext dataContext=new appDBDataContext();
        public affiliationWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var newAfilliation=new Afilieri { Nume=nameBox.Text};

            dataContext.Afilieris.InsertOnSubmit(newAfilliation);
            dataContext.SubmitChanges();

            this.Close();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
