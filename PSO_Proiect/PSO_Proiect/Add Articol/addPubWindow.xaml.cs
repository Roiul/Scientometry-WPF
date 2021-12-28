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

        int index = 0;
        List<pageData> articole=new List<pageData>();
        List<authorD> Authors=new List<authorD>();
        public addPubWindow()
        {
            InitializeComponent();

            initializeUCComponents();
        }

        public void updateData()
        {
            foreach(var autor in articole[index].autori)
            {
                authorsDataGrid.Items.Add(autor);
                Authors.Add(autor);
            }
            if (articole[index].numeArticol != null)
            {
                nameTextBox.Text = articole[index].numeArticol;
                nameTextBox.IsReadOnly = true;
            }
            else
                nameTextBox.Text = string.Empty;
            if (articole[index].numePublicatie != null)
            {
                pubNameTextBox.Text = articole[index].numePublicatie;
                pubNameTextBox.IsReadOnly = true;
            }
            else
                pubNameTextBox.Text = string.Empty;
            if (articole[index].editorPublicatie != null)
            {
                editorTextBox.Text = articole[index].editorPublicatie;
                editorTextBox.IsReadOnly = true;
            }
            else
                editorTextBox.Text = string.Empty;
            if (articole[index].jurnalArticol != null)
            {
                jurnalTextBox.Text = articole[index].jurnalArticol;
                jurnalTextBox.IsReadOnly = true;
            }
            else
                jurnalTextBox.Text = string.Empty;
            if (articole[index].volumArticol != null)
            {
                volumeTextBox.Text = articole[index].volumArticol;
                volumeTextBox.IsReadOnly = true;
            }
            else
                volumeTextBox.Text = string.Empty;
            if (articole[index].paginiArticol != null)
            {
                pageTextBox.Text = articole[index].paginiArticol;
                pageTextBox.IsReadOnly = true;
            }
            else
                pageTextBox.Text = string.Empty;
            if (articole[index].numarArticol != null)
            {
                numberTextBox.Text = articole[index].numarArticol;
                numberTextBox.IsReadOnly = true;
            }
            else
                numberTextBox.Text = string.Empty;
            if (articole[index].doiArticol != null)
            {
                doiTextBox.Text = articole[index].doiArticol;
                doiTextBox.IsReadOnly = true;
            }
            if (articole[index].anArticol != null)
            {
                yearTextBox.Text = articole[index].anArticol;
                yearTextBox.IsReadOnly = true;
            }
            else
                yearTextBox.Text = string.Empty;
            if (articole[index].tipPublicatie != null)
            {
                typeComboBox.Text = articole[index].tipPublicatie;
                typeComboBox.IsReadOnly = true;
            }
            else
                typeComboBox.Text = string.Empty;
            if (articole[index].modPrezentare != null)
            {
                modeComboBox.Text = articole[index].modPrezentare;
                modeComboBox.IsReadOnly = true;
            }
            else
                modeComboBox.Text = string.Empty;
            impactFactorTextBox.Text = string.Empty;
        index++;
        }

        public void insertFromBibTex(BibtexIntroduction.BibtexFile file, int variable)
        {
            pageData newArticol = new pageData();
            newArticol.autori = new List<authorD>();
            for (int counter = 0; counter < file.Entries.ToList()[variable].Tags.Count; counter++)
            {
                if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "author")
                {
                    for (int j = 0; j < file.Entries.ToList()[variable].Tags.ToList()[counter].Value.Split(new[] { "and" }, StringSplitOptions.None).Count(); j++)
                    {
                        authorD author = new authorD();
                        author.lName = file.Entries.ToList()[variable].Tags.ToList()[counter].Value.Split(new[] { "and" }, StringSplitOptions.None).ToList()[j].Split(',').ToList()[0];
                        author.fName = file.Entries.ToList()[variable].Tags.ToList()[counter].Value.Split(new[] { "and" }, StringSplitOptions.None).ToList()[j].Split(',').ToList()[1];

                        var authorFromDB = (from autor in db.Autoris
                                            where autor.Nume + " " + autor.Prenume[0] == author.lName + " " + author.lName[0]
                                            select autor).FirstOrDefault();
                        if (authorFromDB == null)
                        {
                            var newAutor=new Autori { Nume = author.lName, Prenume = author.fName };
                            author.fromDatabase = false;
                            author.IsSelected = false;
                            db.Autoris.InsertOnSubmit(newAutor);
                            db.SubmitChanges();
                            author.idAuthor = newAutor.IDAutor;
                        }
                        else
                        {
                            author.fName = authorFromDB.Nume;
                            author.lName = authorFromDB.Prenume;
                            author.idAuthor = authorFromDB.IDAutor;
                            author.IsSelected = false;
                            author.fromDatabase = true;
                            if (authorFromDB.Link != null)
                                author.link = authorFromDB.Link;
                            else
                                author.link = string.Empty;
                            if (authorFromDB.UEFID != null)
                                author.uefid = authorFromDB.UEFID;
                            else
                                author.uefid = string.Empty;
                        }
                        newArticol.autori.Add(author);
                    }
                    continue;
                }
                if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "title")
                {
                    newArticol.numeArticol= file.Entries.ToList()[variable].Tags.ToList()[counter].Value;                    
                    continue;
                }
                if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "publisher")
                {
                    newArticol.numePublicatie = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;                    
                    continue;
                }
                if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "editor")
                {
                    newArticol.editorPublicatie = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;
                    
                    continue;
                }
                if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "journal")
                {
                    newArticol.jurnalArticol = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;                   
                    continue;
                }
                if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "year")
                {
                    newArticol.anArticol = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;
                    continue;
                }
                if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "volume")
                {
                    newArticol.volumArticol = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;                   
                    continue;
                }
                if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "pages")
                {
                    newArticol.paginiArticol = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;                   
                    continue;
                }
                if (file.Entries.ToList()[variable].Tags.ToList()[counter].Key == "number")
                {
                    newArticol.numarArticol = file.Entries.ToList()[variable].Tags.ToList()[counter].Value;                   
                    continue;
                }
            }
            articole.Add(newArticol);
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
            if (this.impactFactorTextBox.Text == "")
                this.impactFactorTextBox.Text = "0";
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
                    newAuthor.uefid = newAdded[newAdded.Count - 1].UEFID;
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
                newAuthor.uefid = author.UEFID;
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
            authorsDataGrid.Items.Clear();
            Authors.Clear();
            if (articole.Count > index)
            {
                updateData();
                MessageBox.Show("Articol adaugat cu succes!\nMai sunt de adaugat " + (articole.Count - index + 1) + " articole", "Succes", MessageBoxButton.OK, MessageBoxImage.Information); ;
            }
            else
            {
                MessageBox.Show("Articol adaugat cu succes", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                backFromPubsButtonAction();
            }       
        }

        private bool verifyData()
        {
            if (pubNameTextBox.Text == "")
            {
                MessageBox.Show("Nu ati adaugat numele editurii!\nToate campurile marcate cu * trebuie completate.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (editorTextBox.Text == "")
            {
                MessageBox.Show("Nu ati adaugat numele editorului!\nToate campurile marcate cu * trebuie completate.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (typeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Nu ati selectat tipul editurii!\nToate campurile marcate cu * trebuie completate.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (nameTextBox.Text == "")
            {
                MessageBox.Show("Nu ati adaugat numele articolului!\nToate campurile marcate cu * trebuie completate.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (modeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Nu ati adaugat numele articolului!\nToate campurile marcate cu * trebuie completate.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            int IsSelected = -1;
            foreach (var author in Authors)
                if (author.IsSelected == true)
                    IsSelected++;
            switch (IsSelected)
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
    public class pageData
    {
        public string numePublicatie { get; set; }
        public string editorPublicatie { get; set; }
        public string tipPublicatie { get; set; }
        public string numeArticol { get; set; }
        public string doiArticol { get; set; }
        public string jurnalArticol { get; set; }
        public string modPrezentare { get; set; }
        public List<authorD> autori { get; set; }
        public string anArticol { get; set; }
        public string paginiArticol { get; set; }
        public string volumArticol { get; set; }
        public string numarArticol { get; set; }
    }
    public class authorD
    {
        public bool fromDatabase { get; set; }
        public bool IsSelected { get; set; }
        public int idAuthor { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string uefid { get; set; }
        public string link { get; set; }
    }
}
