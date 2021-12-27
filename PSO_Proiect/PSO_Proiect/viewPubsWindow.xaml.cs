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
    /// <summary>
    /// Interaction logic for viewPubsWindow.xaml
    /// </summary>
    public partial class viewPubsWindow : UserControl
    {
        public Action<BibtexIntroduction.BibtexFile> insertFromBib;

        public Action viewPubsButtonAction;
        public Action addButtonAction;
        public Action exitButtonAction;
        public viewPubsWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.exitButtonAction();
        }

        private void BibTextButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName=="")
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
    }
}
