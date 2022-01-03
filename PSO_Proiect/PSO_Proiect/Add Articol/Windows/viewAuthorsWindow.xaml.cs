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
    /// Interaction logic for viewAuthorsWindow.xaml
    /// </summary>
    public partial class viewAuthorsWindow : UserControl
    {
        appDBDataContext db = new appDBDataContext();

        public Action backToViewPubs;

        private List<Autor> updateAutors=new List<Autor>();
        private List<Autor> autors = new List<Autor>();
        public viewAuthorsWindow()
        {
            InitializeComponent();

            firstSearch();
        }
        private void firstSearch()
        {
            autors.Clear();
            authorsDataGrid.ItemsSource = null;

            var autori = (from items in db.Autoris
                          select items).ToList();
            foreach (var item in autori)
            {
                Autor autor = new Autor();

                autor.UEFID = item.UEFID;
                autor.Nume = item.Nume;
                autor.Prenume = item.Prenume;
                autor.Link = item.Link;
                autor.ID = item.IDAutor;
                var idAfilieri = (from item1 in db.Autor_Afilieres
                                  where item1.IDAutor == item.IDAutor
                                  select item1.IDAfiliere).ToList();
                for(int i = 0; i < idAfilieri.Count; i++)
                {
                    var numeAfiliere = (from item2 in db.Afilieris
                                        where item2.IDAfiliere == idAfilieri[i]
                                        select item2).FirstOrDefault();
                    if (i == 0)
                        autor.Afilieri += numeAfiliere.Nume;
                    else
                        autor.Afilieri += "\n" + numeAfiliere.Nume;
                }
                autors.Add(autor);
            }
            authorsDataGrid.ItemsSource = autors;
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            autors.Clear();
            authorsDataGrid.ItemsSource = null;

            var autori = (from items in db.Autoris
                          where items.Nume + " " + items.Prenume == searchTextBox.Text
                          select items).ToList();
            foreach (var item in autori)
            {
                Autor autor = new Autor();

                autor.ID = item.IDAutor;
                autor.UEFID = item.UEFID.ToString();
                autor.Nume = item.Nume;
                autor.Prenume = item.Prenume;
                autor.Link = item.Link;
                var idAfilieri = (from item1 in db.Autor_Afilieres
                                  where item1.IDAutor == item.IDAutor
                                  select item1.IDAfiliere).ToList();
                for (int i = 0; i < idAfilieri.Count; i++)
                {
                    var numeAfiliere = (from item2 in db.Afilieris
                                        where item2.IDAfiliere == idAfilieri[i]
                                        select item2).FirstOrDefault();
                    if (i == 0)
                        autor.Afilieri += numeAfiliere.Nume;
                    else
                        autor.Afilieri += "\n" + numeAfiliere.Nume;
                }
                autors.Add(autor);
            }
            authorsDataGrid.ItemsSource = autors;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (updateAutors.Count > 0)
                if (MessageBox.Show("UPDATE neinregistrat!\nInregistrati schimbarile?", "Alerta!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    update();
            autors.Clear();
            authorsDataGrid.ItemsSource = null;
            backToViewPubs();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if(updateAutors.Count > 0)
                if(MessageBox.Show("UPDATE neinregistrat!\nInregistrati schimbarile?", "Alerta!", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                    update();
            firstSearch();
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            authorWindow authorW =new authorWindow();
            authorW.ShowDialog();

            firstSearch();
        }

        private void authorsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Autor temp = e.Row.DataContext as Autor;
            updateAutors.Add(temp);
        }
        private void update()
        {
            foreach(var temp in updateAutors)
            {
                Autori nUser = db.Autoris.Single(u => u.IDAutor == temp.ID);
                nUser.IDAutor = temp.ID;
                nUser.UEFID = temp.UEFID;
                nUser.Nume = temp.Nume;
                nUser.Prenume = temp.Prenume;
                nUser.Link = temp.Link;
                db.SubmitChanges();
            }
            updateAutors.Clear();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            update();
        }

        private void deleteRecord_Click(object sender, RoutedEventArgs e)
        {
            foreach(var row in authorsDataGrid.SelectedItems)
            {
                Autor temp = (Autor)row;
                var articoleAutor=(from articole in db.Autori_Articoles
                                   where articole.IDAutor==temp.ID
                                   select articole).ToList();
                if (articoleAutor != null)
                {
                    if (MessageBox.Show("Autorul " + temp.Nume + " " + temp.Prenume + " are articole inregistrate!\nSunteti sigur ca doriti sa-l stergeti?", "Eroare", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        db.Autori_Articoles.DeleteAllOnSubmit(articoleAutor);
                        db.SubmitChanges();
                    }
                    else
                        continue;
                }
                Autori dUser = db.Autoris.Single(u => u.IDAutor == temp.ID);
                var autoriAfilieri = (from item in db.Autor_Afilieres
                                      where item.IDAutor == temp.ID
                                      select item).ToList();
                db.Autor_Afilieres.DeleteAllOnSubmit(autoriAfilieri);
                db.SubmitChanges();
                db.Autoris.DeleteOnSubmit(dUser);
            }
            db.SubmitChanges();
            MessageBox.Show("Stergere realizata!", "Succes");
            firstSearch();
        }
    }

    public class Autor
    {
        public int ID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string UEFID { get; set; }
        public string Link { get; set; }
        public string Afilieri { get; set; }
    }
}
