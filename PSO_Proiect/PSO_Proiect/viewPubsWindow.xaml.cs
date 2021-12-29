using BibtexIntroduction;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace PSO_Proiect
{
    /// <summary>
    /// Interaction logic for viewPubsWindow.xaml
    /// </summary>
    public partial class viewPubsWindow : UserControl
    {
        appDBDataContext db = new appDBDataContext();

        public Action viewPubsButtonAction;
        public Action addButtonAction;
        public Action exitButtonAction;
        public Action<string,string, int,string> getPubs;

        //private List<String> filterConstraints;
        //private DataTable dt;
        //private DataView dv;
        //private List<Articol> articole;

        public viewPubsWindow()
        {
            InitializeComponent();

            //#region comboBox(filtru) Afilieri
            //var afilieri = (from item in db.Afilieris
            //                select item.Nume).ToList().Distinct();
            //List<string> listaAfilieri=new List<string>();

            //foreach(var item in afilieri)
            //{
            //    string a = "";
            //    a = item;
            //    listaAfilieri.Add(a);
            //}
            //afiliereComboBox.ItemsSource=listaAfilieri;
            //#endregion

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
            List<int> listaAni=new List<int>();
            
            foreach (var item in ani)
            {
                int a = 0;
                a = item.Value;
                listaAni.Add(a);
            }
           
            anComboBox.ItemsSource=listaAni;
            #endregion

            #region comboBox(filtru) Autori
            var autori = (from item in db.Autoris
                        select new {Nume = item.Nume, Prenume=item.Prenume}).ToList().Distinct();
            List<string> listaAutori = new List<string>();
            
            foreach (var item in autori)
            {
                string a = "";
                a = item.Nume+" "+item.Prenume;
                listaAutori.Add(a);
            }
            autorComboBox.ItemsSource = listaAutori;
            #endregion

            #region populare Articole
            var articole = (from item in db.Articoles
                            select item.Nume).ToList().Distinct();
            List<Articol> listaArticole = new List<Articol>();

            foreach (var item in articole)
            {
                //    Articol a = new Articol();
                //    a.Afiliere = (from i in db.Afilieris
                //                  join j in db.Autor_Afilieres on i.IDAfiliere equals j.IDAfiliere
                //                  join k in db.Autoris on j.IDAutor equals k.IDAutor
                //                  join l in db.Autori_Articoles on k.IDAutor equals l.IDAutor
                //                  join m in db.Articoles on l.IDArticol equals m.IDArticol
                //                  select i.Nume.ToString()).Single();
                //    //a.Afiliere = (from i in db.Articoles
                //    //              join j in db.Autori_Articoles on i.IDArticol equals j.IDArticol
                //    //              join k in db.Autoris on i.IDAutor equals k.IDAutor
                //    //              join l in db.Autor_Afilieres on i.IDAutor equals l.IDAutor
                //    //              join m in db.Afilieris on i.IDAfiliere equals m.IDAfiliere
                //    //              select i.Nume.ToString);
                //    //a.Afiliere = (from i in db.Afilieris
                //    //              select i.Nume).Single();
                //    //a.TipPublicatie = (from i in db.Tip_Publicaties
                //    //                   join j in db.Publicatiis on i.Tip equals j.TipPublicatie
                //    //                   join k in db.Articoles on j.IDPublicatie equals k.IDPublicatie
                //    //                   select i.Tip.ToString()).Single();
                //    listaArticole.Add(a);

                //Articol articol = new Articol();
                //articol.TipPublicatie = (from i in db.Tip_Publicaties
                //                         join j in db.Publicatiis on i.IDTipPublicatie equals j.IDPublicatie
                //                         join k in db.Articoles on j.IDPublicatie equals k.IDPublicatie
                //                         select i.Tip).Single().ToString();

                Articol articol = new Articol();
                articol.Autor = (from i in db.Autoris
                                 join j in db.Autori_Articoles on i.IDAutor equals j.IDAutor
                                 join k in db.Articoles on j.IDAutor equals k.IDAutor
                                 select i.Nume).Single().ToString();

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

            if (openFileDialog.CheckFileExists)
                return;
            string fileName = openFileDialog.FileName;
            string content = File.ReadAllText(fileName);

            BibtexFile file = BibtexIntroduction.BibtexImporter.FromString(content);

            Console.WriteLine(file.Entries.Count);
        }

        private void ManualButton_Click(object sender, RoutedEventArgs e)
        {
            this.addButtonAction();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void afiliereComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        public string TipPublicatie { get; set; }
        public int An { get; set; }
        public string Autor { get; set; }

    }
}
