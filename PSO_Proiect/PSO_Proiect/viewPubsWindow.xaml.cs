using BibtexIntroduction;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// </summary>
    public partial class viewPubsWindow : UserControl
    {
        appDBDataContext db = new appDBDataContext();
        public Action<BibtexIntroduction.BibtexFile> insertFromBib;
        public Action viewPubsButtonAction;
        public Action viewAuthorsButtonAction;
        public Action addButtonAction;
        public Action exitButtonAction;
        public Action<string, string, int, string> getPubs;

        public static Tip_Publicatie tip= new Tip_Publicatie();
        public static Detalii an = new Detalii();
        public static Autori numeAutor=new Autori();

        private List<Articol> listaArticole = new List<Articol>();
        public viewPubsWindow()
        {
            InitializeComponent();
            reset();

            #region comboBox(filtru) TipPublicatie
            var publicatii = (from item in db.Tip_Publicaties
                              select item.Tip).ToList().Distinct();
            List<string> listaPublicatii = new List<string>();

            foreach (var item in publicatii)
            {
                string p = "";
                p = item;
                listaPublicatii.Add(p);
            }
            tipPublicatieComboBox.ItemsSource = listaPublicatii;
            
            #endregion

            #region comboBox(filtru) Ani
            var ani = (from item in db.Detaliis
                       select item.An).ToList().Distinct();
            List<int> listaAni = new List<int>();

            foreach (var item in ani)
            {
                int a = 0;
                a = item;
                listaAni.Add(a);
            }
            anComboBox.ItemsSource = listaAni;
            #endregion

            #region comboBox(filtru) Autori
            var autori = (from item in db.Autoris
                          select new { Nume = item.Nume, Prenume = item.Prenume }).ToList().Distinct();
            List<string> listaAutori = new List<string>();

            foreach (var item in autori)
            {
                string a = "";
                a = item.Nume + " " + item.Prenume;
                listaAutori.Add(a);
            }
            autorComboBox.ItemsSource = listaAutori;
            #endregion
        }

        private void reset()
        {
            tip.Tip = null;
            tipPublicatieComboBox.Text = "";

            an.An = 0;
            anComboBox.Text = "";

            numeAutor.Nume = null;
            autorComboBox.Text = "";

            listaArticole.Clear();
            pubsDataGrid.ItemsSource = null;

            var articole = (from items in db.Articoles
                            join j in db.Publicatiis on items.IDPublicatie equals j.IDPublicatie
                            join k in db.Tip_Publicaties on j.TipPublicatie equals k.IDTipPublicatie
                            select items).ToList();

            foreach (var item in articole)
            {
                Articol articol = new Articol();

                articol.Nume = item.Nume;

                articol.An = (from i in db.Detaliis
                              where i.IDDetalii == item.IDDetalii
                              select i.An).FirstOrDefault();

                articol.Autor = (from i in db.Autoris
                                 join j in db.Autori_Articoles on i.IDAutor equals j.IDAutor
                                 join k in db.Articoles on j.IDArticol equals k.IDArticol
                                 select i.Nume).FirstOrDefault();

                articol.TipPublicatie = (from i in db.Tip_Publicaties
                                         join j in db.Publicatiis on i.IDTipPublicatie equals j.TipPublicatie
                                         join k in db.Articoles on j.IDPublicatie equals k.IDPublicatie
                                         select i.Tip).FirstOrDefault();

                listaArticole.Add(articol);
            }
            pubsDataGrid.ItemsSource = listaArticole;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.exitButtonAction();
        }

        private void BibTextButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName == "")
                return;
            string fileName = openFileDialog.FileName;
            string content = File.ReadAllText(fileName);

            BibtexFile file = BibtexIntroduction.BibtexImporter.FromString(content);

            insertFromBib(file);
        }

        private void ManualButton_Click(object sender, RoutedEventArgs e)
        {
            this.addButtonAction();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void authorsButton_Click(object sender, RoutedEventArgs e)
        {
            viewAuthorsButtonAction();
        }

        private void tipPublicatieComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listaArticole.Clear();

            var selectedAn = anComboBox.SelectedItem;
            an.An = Convert.ToInt32(selectedAn);

            string selectedPub = (string)tipPublicatieComboBox.SelectedItem;
            tip.Tip = selectedPub;

            string selectedAutor = (string)autorComboBox.SelectedItem;
            numeAutor.Nume = selectedAutor;
            
            filter(selectedPub, an.An, selectedAutor);

        }

        private void anComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listaArticole.Clear();

            var selectedAn = anComboBox.SelectedItem;
            an.An = Convert.ToInt32(selectedAn);

            string selectedPub = (string)tipPublicatieComboBox.SelectedItem;
            tip.Tip = selectedPub;

            string selectedAutor = (string)autorComboBox.SelectedItem;
            numeAutor.Nume = selectedAutor;

            filter(selectedPub, an.An, selectedAutor);
        }

        private void autorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listaArticole.Clear();

            var selectedAn = anComboBox.SelectedItem;
            an.An = Convert.ToInt32(selectedAn);

            string selectedPub = (string)tipPublicatieComboBox.SelectedItem;
            tip.Tip = selectedPub;

            string selectedAutor = (string)autorComboBox.SelectedItem;
            numeAutor.Nume = selectedAutor;

            filter(selectedPub, an.An, selectedAutor);
        }

        private void filter(string tipPublicatie, int an, string autor)
        {
            #region filtru tip publicatie
            pubsDataGrid.ItemsSource = null;
            if (!string.IsNullOrEmpty(tipPublicatie))
            {
                var articole = (from items in db.Articoles
                                join j in db.Publicatiis on items.IDPublicatie equals j.IDPublicatie
                                join k in db.Tip_Publicaties on j.TipPublicatie equals k.IDTipPublicatie
                                where k.Tip == tipPublicatie
                                select items).ToList();

                foreach (var item in articole)
                {
                    Articol articol = new Articol();

                    articol.Nume = item.Nume;

                    articol.An = (from i in db.Detaliis
                                  where i.IDDetalii== item.IDDetalii
                                  select i.An).FirstOrDefault();

                    articol.Autor = (from i in db.Autoris
                                     join j in db.Autori_Articoles on i.IDAutor equals j.IDAutor
                                     join k in db.Articoles on j.IDArticol equals k.IDArticol
                                     select i.Nume).FirstOrDefault();

                    articol.TipPublicatie = tipPublicatie;

                    listaArticole.Add(articol);
                }
                if(an!=0)
                {
                    List<Articol> list = new List<Articol>();
                    foreach(var item in listaArticole)
                    {
                        if(item.An != an)
                            list.Add(item);
                            
                    }
                    foreach(var articol in list)
                    {
                        listaArticole.Remove(articol);
                    }
                    list.Clear();
                }
                if(!string.IsNullOrEmpty(autor))
                {
                    List<Articol> listAutori = new List<Articol>();
                    foreach (var item in listaArticole)
                    {
                        if (item.Autor != autor)
                            listAutori.Add(item);

                    }
                    foreach (var articol in listAutori)
                    {
                        listaArticole.Remove(articol);
                    }
                    listAutori.Clear();
                }    
            }
            pubsDataGrid.ItemsSource = listaArticole;
            #endregion
            #region filtru an
            #endregion
            #region filtru autor
            #endregion
        }
    }

    class Articol
    {
        public string Nume { get; set; }
        public string TipPublicatie { get; set; }
        public int An { get; set; }
        public string Autor { get; set; }
    }
}
