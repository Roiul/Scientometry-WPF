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

        List<authorD> Authors=new List<authorD>();
        public addPubWindow()
        {
            InitializeComponent();

            initializeUCComponents();
        }

        private int publicatiiTableInsert()
        {
            var type = (from item in db.Tip_Publicaties
                        where item.Tip == typeComboBox.SelectedItem.ToString()
                        select item).FirstOrDefault();
            var newPublicatie = new Publicatii
            {
                Nume = this.pubNameTextBox.Text,
                Editor = this.editorTextBox.Text,
                TipPublicatie = type.IDTipPublicatie
            };
            db.Publicatiis.InsertOnSubmit(newPublicatie);
            db.SubmitChanges();
            return newPublicatie.IDPublicatie;
        }
        private int detaliiTableInsert()
        {
            var newDetalii = new Detalii
            {
                An = Convert.ToInt32(this.yearTextBox.Text),
                Pagina = Convert.ToInt32(this.pageTextBox.Text),
                Volum = this.volumeTextBox.Text,
                Numar = Convert.ToInt32(this.numberTextBox.Text)
            };
            db.Detaliis.InsertOnSubmit(newDetalii);
            db.SubmitChanges();
            return newDetalii.IDDetalii;
        }
        private int articoleTableInsert(int idPublicatie, int idDetalii)
        {
            var idMod=(from item in db.ModPrezentares
                       where item.Tip==this.modeComboBox.Text
                       select item).FirstOrDefault();
            var newArticol = new Articole
            {
                Nume = this.nameTextBox.Text,
                FactorImpact = Convert.ToInt32(this.impactFactorTextBox.Text),
                WOS = this.wosTextBox.Text,
                DOI = this.doiTextBox.Text,
                IDDetalii = idDetalii,
                IDPublicatie = idPublicatie,
                IDMod = idMod.IDMod
            };
            db.Articoles.InsertOnSubmit(newArticol);
            db.SubmitChanges();
            return newArticol.IDArticol;
        }
        private void articoleAutoriTableInsert(int idArticol)
        {
            foreach(var author in Authors)
            {
                var newAutoriArticole = new Autori_Articole
                {
                    IDArticol = idArticol,
                    IDAutor = author.idAuthor
                };
                db.Autori_Articoles.InsertOnSubmit(newAutoriArticole);
            }
            db.SubmitChanges();
        }
        private void initializeUCComponents()
        {
            var mods = (from moduri in db.ModPrezentares
                        select moduri.Tip).ToList();
            this.modeComboBox.ItemsSource = mods;

            var type = (from items in db.Tip_Publicaties
                        select items.Tip).ToList();

            typeComboBox.ItemsSource = type;

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
                    newAuthor.idAuthor = newAdded[newAdded.Count - 1].IDAutor;
                    Authors.Add(newAuthor);
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
                newAuthor.idAuthor = author.IDAutor;
                authorsDataGrid.Items.Add(newAuthor);
                Authors.Add(newAuthor);
            }
        }

        private void deleteRecord_Click(object sender, RoutedEventArgs e)
        {
            while (authorsDataGrid.SelectedItems.Count >= 1)
                authorsDataGrid.Items.Remove(authorsDataGrid.SelectedItem);
        }

        private void addArticolButton_Click(object sender, RoutedEventArgs e)
        {
            if (!verifyData())
                return;

            int isSeleced = -1;
            foreach (var author in Authors)
                if(author.IsSelected==true)
                    isSeleced++;
            switch (isSeleced)
            {
            case 0:
                articoleAutoriTableInsert(
                    articoleTableInsert(
                        publicatiiTableInsert(),
                        detaliiTableInsert()
                    ));
                break;
            case -1:
                MessageBox.Show("Nu ati selectat autorul principal!", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                break;
            default:
                MessageBox.Show("Ati adaugat mai mult de un autor principal", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                break;
            }
        }

        private bool verifyData()
        {
            return true;
        }
    }
    public class authorD
    {
        public bool IsSelected { get; set; }
        public int idAuthor { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public int uefid { get; set; }
        public string link { get; set; }
    }
}
