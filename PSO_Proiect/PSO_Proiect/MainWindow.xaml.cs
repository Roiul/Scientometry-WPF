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
        viewAuthorsWindow viewAuthorsW=new viewAuthorsWindow();
        
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
        public void addNewPubFromBibTex(BibtexIntroduction.BibtexFile file)
        {
                this.masterGrid.Children.Clear();
                this.masterGrid.Children.Add(addPubW);
                for (int i = 0; i < file.Entries.Count; i++)
                    addPubW.insertFromBibTex(file, i);
                addPubW.updateData();
        }
        public void viewAuthors()
        {
            this.masterGrid.Children.Clear();
            this.masterGrid.Children.Add(viewAuthorsW);
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
            viewPubsW.insertFromBib += addNewPubFromBibTex;
            viewPubsW.viewAuthorsButtonAction += viewAuthors;

            addPubW.backFromPubsButtonAction += viewPubs;

            viewAuthorsW.backToViewPubs += viewPubs;
        }
    }
}
