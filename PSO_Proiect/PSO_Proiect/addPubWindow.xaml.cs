﻿using System;
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
        public appDBDataContext appDB = new appDBDataContext();

        public Action backFromPubsButtonAction;
        public addPubWindow()
        {
            InitializeComponent();

            var mods = (from moduri in appDB.ModPrezentare1s
                        select moduri.Tip).ToList();

            this.modeComboBox.ItemsSource=mods;
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
    }
}
