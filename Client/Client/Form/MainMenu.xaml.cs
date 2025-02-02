﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.ServiceModel;


namespace Form
{
    /// <summary>
    /// Inter_action logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public static Frame MenuFrame;

        public MainMenu()
        {
            InitializeComponent();
            MenuFrame = MainMenuFrame;
            MenuFrame.Navigate(new MenuPage());
        }

        public MainMenu(bool failed)
        {
            InitializeComponent();
            MenuFrame = MainMenuFrame;
            MenuFrame.Navigate(new MenuPage(failed));
        }

    }
}
