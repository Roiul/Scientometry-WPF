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

        public void insertFromBibTex(BibtexIntroduction.BibtexFile file)
        {
            for (int variable = 0; variable < file.Entries.Count; variable++)
            {
                for (int counter = 0; counter < file.Entries.ToList()[variable].Tags.Count; counter++)
                {
                    if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "author")
                    {
                        for(int j=0;j< file.Entries.ToList()[variable].Tags.ToList()[counter].Value.Split(',').Count();j++)
                        {
                            authorD author=new authorD();
                            author.fName = file.Entries.ToList()[variable].Tags.ToList()[counter].Value.Split(',').ToList()[j];

                            authorsDataGrid.Items.Add(author);
                            Authors.Add(author);
                        }

                        nameTextBox.Text = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;
                        nameTextBox.IsReadOnly = true;
                        continue;
                    }
                    if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "title")
                    {
                        nameTextBox.Text = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;
                        nameTextBox.IsReadOnly = true;
                        continue;
                    }
                    if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "publisher")
                    {
                        pubNameTextBox.Text = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;
                        pubNameTextBox.IsReadOnly=true;
                        continue;
                    }
                    if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "editor")
                    {
                        editorTextBox.Text = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;
                        editorTextBox.IsReadOnly = true;
                        continue;
                    }
                    if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "journal")
                    {
                        jurnalTextBox.Text = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;
                        jurnalTextBox.IsReadOnly = true;
                        continue;
                    }
                    if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "year")
                    {
                        yearTextBox.Text = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;
                        yearTextBox.IsReadOnly=true;
                        continue;
                    }
                    if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "volume")
                    {
                        volumeTextBox.Text = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;
                        volumeTextBox.IsReadOnly=true;
                        continue;
                    }
                    if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "pages")
                    {
                        pageTextBox.Text = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;
                        pageTextBox.IsReadOnly=true;
                        continue;
                    }
                    if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "number")
                    {
                        numberTextBox.Text = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;
                        numberTextBox.IsReadOnly = true;
                        continue;
                    }
                }
                continue;
            }
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
                Pagina = this.pageTextBox.Text,
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
                    IDAutor = author.idAuthor,
                    TipAutor=Convert.ToInt32(author.IsSelected)
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
                    newAuthor.fromDatabase = true;
                    authorsDataGrid.Items.Add(newAuthor);
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
                newAuthor.fromDatabase= true;
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
            articoleAutoriTableInsert(
                       articoleTableInsert(
                           publicatiiTableInsert(),
                           detaliiTableInsert()
                       ));
            MessageBox.Show("Articol adaugat cu succes", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            backFromPubsButtonAction();
        }

        private bool verifyData()
        {
            if (pubNameTextBox.Text == "")
            {
                MessageBox.Show("Nu ati adaugat numele editurii!", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (editorTextBox.Text == "")
            {
                MessageBox.Show("Nu ati adaugat numele editorului!", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (typeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Nu ati selectat tipul editurii!", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (nameTextBox.Text == "")
            {
                MessageBox.Show("Nu ati adaugat numele articolului!", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (modeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Nu ati adaugat numele articolului!", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            int isSeleced = -1;
            foreach (var author in Authors)
                if (author.IsSelected == true)
                    isSeleced++;
            switch (isSeleced)
            {
                case 0:
                    break;
                case -1:
                    MessageBox.Show("Nu ati selectat autorul principal!", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                default:
                    MessageBox.Show("Ati adaugat mai mult de un autor principal", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
            }

            return true;
        }
    }
    public class authorD
    {
        public bool fromDatabase { get; set; }
        public bool IsSelected { get; set; }
        public int idAuthor { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public int uefid { get; set; }
        public string link { get; set; }
    }
}
