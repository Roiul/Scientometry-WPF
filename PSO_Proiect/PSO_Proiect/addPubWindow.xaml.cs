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
    /// Interaction logic for addPubWindow.xaml
    /// </summary>
    public partial class addPubWindow : UserControl
    {
        public appDBDataContext db = new appDBDataContext();

        public Action backFromPubsButtonAction;
        public addPubWindow()
        {
            InitializeComponent();

            var mods = (from moduri in db.ModPrezentares
                        select moduri.Tip).ToList();
            this.modeComboBox.ItemsSource=mods;

            updateAuthorComboBox();
        }
        private void updateAuthorComboBox()
        {
            var authors = (from autor in db.Autoris
                           select autor).ToList();
            List<string> authorsList = new List<string>();
            authorsList.Add("Adaugare Nou");
            foreach (var author in authors)
                authorsList.Add(author.Nume +" "+ author.Prenume);

            authorComboBox.ItemsSource = authorsList;
        }

        private void pubTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            pubDetailsWindow pubDetailsWindow = new pubDetailsWindow();
            pubDetailsWindow.Show();
        }

        private void backToMainButton_Click(object sender, RoutedEventArgs e)
        {
            this.backFromPubsButtonAction();
        }

        private void authorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = authorComboBox.SelectedItem;
            authorD newAuthor = new authorD();
            if (selectedItem.ToString() == "Adaugare Nou")
            {
                var lastAdded = (from item in db.Autoris
                                 select item).ToList();
                authorWindow authorWindow = new authorWindow();
                authorWindow.ShowDialog();
                updateAuthorComboBox();

                var newAdded = (from item in db.Autoris
                                select item).ToList();
                if (lastAdded.Count != newAdded.Count)
                {
                    authorComboBox.SelectedItem = newAdded[newAdded.Count - 1].Nume+ " "+
                        newAdded[newAdded.Count - 1].Prenume;
                    newAuthor.fName = newAdded[newAdded.Count - 1].Prenume;
                    newAuthor.lName = newAdded[newAdded.Count -1].Nume;
                    newAuthor.uefid = (int)newAdded[newAdded.Count - 1].UEFID;
                    newAuthor.link= newAdded[newAdded.Count - 1].Link;
                }
            }
            else
            {
                var author = (from item in db.Autoris
                              where (item.Nume+" "+item.Prenume) == selectedItem.ToString()
                              select item).FirstOrDefault();
                newAuthor.fName = author.Prenume;
                newAuthor.lName = author.Nume;
                newAuthor.uefid = (int)author.UEFID;
                newAuthor.link = author.Link;
                authorsDataGrid.Items.Add(newAuthor);
            }
        }

        private void deleteRecord_Click(object sender, RoutedEventArgs e)
        {
            while (authorsDataGrid.SelectedItems.Count >= 1)
                authorsDataGrid.Items.Remove(authorsDataGrid.SelectedItem);
        }
    }
    public class authorD
    {
        public string fName { get; set; }
        public string lName { get; set; }
        public int uefid { get; set; }
        public string link { get; set; }
    }
}
