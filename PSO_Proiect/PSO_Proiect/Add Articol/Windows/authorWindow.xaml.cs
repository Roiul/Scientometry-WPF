using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for authorWindow.xaml
    /// </summary>
    public partial class authorWindow : Window
    {
        appDBDataContext db=new appDBDataContext();
        public authorWindow()
        {
            InitializeComponent();
            updateAffiliationComboBox();
        }

        private void updateAffiliationComboBox()
        {
            List<string> affiliationList = new List<string>();
            affiliationList.Add("Adaugare Nou");

            var affiliations = (from item in db.Afilieris
                                select item).ToList();
            foreach (var affiliation in affiliations)
                affiliationList.Add(affiliation.Nume);

            affiliationComboBox.ItemsSource = affiliationList;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void affiliationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem= affiliationComboBox.SelectedItem;
            affiliationNames affiliation=new affiliationNames();
            if (selectedItem.ToString() == "Adaugare Nou")
            {
                var lastAdded = (from item in db.Afilieris
                                 select item).ToList();
                affiliationWindow affiliationWindow = new affiliationWindow();
                affiliationWindow.ShowDialog();
                updateAffiliationComboBox();

                var newAdded = (from item in db.Afilieris
                                select item).ToList();
                if (lastAdded.Count != newAdded.Count)
                {
                    affiliationComboBox.SelectedItem = newAdded[newAdded.Count - 1].Nume;
                    affiliation.Name = newAdded[newAdded.Count - 1].Nume;
                }
            }
            else
            {
                affiliation.Name = selectedItem.ToString();
                affiliationDataGrid.Items.Add(affiliation);
            }
        }
        public class affiliationNames
        {
            public string Name { get; set; }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            while (affiliationDataGrid.SelectedItems.Count >= 1)
                affiliationDataGrid.Items.Remove(affiliationDataGrid.SelectedItem);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (uefidBox.Text == null)
                uefidBox.Text = string.Empty;
            if (linkBox.Text == null)
                linkBox.Text = string.Empty;
            var newAuthor = new Autori
            {
                Nume = lastNameBox.Text,
                Prenume = firstNameBox.Text,
                UEFID = uefidBox.Text,
                Link = linkBox.Text
            };
            db.Autoris.InsertOnSubmit(newAuthor);
            db.SubmitChanges();
            List<int> ids = new List<int>();
            foreach(affiliationNames item in affiliationDataGrid.Items)
            {
                var idAfiliere=(from afiliere in db.Afilieris
                                where afiliere.Nume==item.Name
                                select afiliere.IDAfiliere).FirstOrDefault();
                var newAfiliereAutori = new Autor_Afiliere
                {
                    IDAfiliere = idAfiliere,
                    IDAutor = newAuthor.IDAutor
                };
                db.Autor_Afilieres.InsertOnSubmit(newAfiliereAutori);

                db.SubmitChanges();

                this.Close();
            }
        }
    }
}
