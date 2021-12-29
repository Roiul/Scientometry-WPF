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

        public viewPubsWindow()
        {
            InitializeComponent();

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
                a = item.Value;
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

            #region populare Articole
            var articole = (from items in db.Articoles
                            select items).ToList();
            List<Articol> listaArticole = new List<Articol>();
            listaArticole.Clear();
            foreach(var item in articole)
            {
                Articol articol = new Articol();
                #region autori
                var idAutori = (from i in db.Autoris
                               join j in db.Autori_Articoles on i.IDAutor equals j.IDAutor
                               join k in db.Articoles on j.IDArticol equals k.IDArticol
                               select i.IDAutor).ToList();

                for(int i=0; i<idAutori.Count;i++)
                {

                    var numeAutori = (from l in db.Autoris
                                      where l.IDAutor == idAutori[i]
                                      select l).FirstOrDefault();

                    if (i==0)
                    {
                        articol.Autor += numeAutori.Nume;
                    }
                    else
                    {
                        articol.Autor += " " + numeAutori.Nume;
                    }
                }
                #endregion


                var idPublicatii = (from i in db.Tip_Publicaties
                                    join j in db.Publicatiis on i.IDTipPublicatie equals j.TipPublicatie
                                    join k in db.Articoles on j.IDPublicatie equals k.IDPublicatie
                                    select i.IDTipPublicatie).ToList();

                for (int i = 0; i < idPublicatii.Count; i++)
                {
                    var tipuriPublicatii = (from l in db.Tip_Publicaties
                                            where l.IDTipPublicatie == idPublicatii[i]
                                            select l).FirstOrDefault();
                    if (i == 0)
                    {
                        articol.TipPublicatie += tipuriPublicatii.Tip;
                    }
                    else
                    {
                        articol.TipPublicatie += " " + tipuriPublicatii.Tip;
                    }
                }

                articol.TipPublicatie = (from i in db.Tip_Publicaties
                                         join j in db.Publicatiis on i.IDTipPublicatie equals j.TipPublicatie
                                         join k in db.Articoles on j.IDPublicatie equals k.IDPublicatie
                                         select i.IDTipPublicatie).ToList().ToString();
                //foreach(var it in listaArticole)
                //{
                //    pubsDataGrid.Columns.Add(it);
                //}
                articol.An = (from i in db.Detaliis
                              join j in db.Articoles on i.IDDetalii equals j.IDDetalii
                              select i.An).ToList();

                articol.Nume=(from i in db.Articoles
                              select i.Nume).ToList().ToString();

                listaArticole.Add(articol);
            }
            pubsDataGrid.ItemsSource = listaArticole; 
            #endregion
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

        }

        private void authorsButton_Click(object sender, RoutedEventArgs e)
        {
            viewAuthorsButtonAction();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tipPublicatieComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void anComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void autorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    class Articol
    {
        public string Nume { get; set; }
        public string TipPublicatie { get; set; }
        public List<int?> An { get; set; }
        public string Autor { get; set; }

    }
}
