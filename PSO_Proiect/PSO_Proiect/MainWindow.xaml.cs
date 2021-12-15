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
    public partial class MainWindow : Window
    {
        public appDBDataContext appDB=new appDBDataContext();

        viewPubsWindow viewPubsW = new viewPubsWindow();
        addPubWindow addPubW = new addPubWindow();
        public MainWindow()
        {
            InitializeComponent();

            this.masterGrid.Children.Add(viewPubsW);
            this.actionMenu();
        }
        public void addNewPub()
        {
            this.masterGrid.Children.Clear();
            this.masterGrid.Children.Add(addPubW);
        }
        public void closeApp()
        {
            this.Close();
        }

        public void viewPubs()
        {
            this.masterGrid.Children.Clear();
            this.masterGrid.Children.Add(viewPubsW);
        }
        public void actionMenu()
        {
            viewPubsW.addButtonAction += addNewPub;
            viewPubsW.exitButtonAction += closeApp;

            addPubW.backFromPubsButtonAction += viewPubs;
        }
    }
}
