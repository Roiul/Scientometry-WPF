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
    /// Interaction logic for startWindow.xaml
    /// </summary>
    public partial class startWindow : UserControl
    {
        public Action viewPubsButtonAction;
        public Action addButtonAction;
        public Action exitButtonAction;
        public startWindow()
        {
            InitializeComponent();
        }

        private void viewPubButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewPubsButtonAction();
        }

        private void newPubButton_Click(object sender, RoutedEventArgs e)
        {
            this.addButtonAction();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.exitButtonAction();
        }
    }
}