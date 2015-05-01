using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace suecaWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PaneSwitcher _paneSwitcher;

        public MainWindow()
        {
            InitializeComponent();
            _paneSwitcher = new PaneSwitcher(SideMenuPaneExpander);
        }

        //TODO: remove me 
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            _paneSwitcher.IncrementPane();
        }

        private void ExpanderChanged(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(SideMenuPaneExpander.IsExpanded);
            LeftSideColumn.Width = SideMenuPaneExpander.IsExpanded ? new GridLength(3, GridUnitType.Star) : new GridLength(23);
            RightSideColumn.Width = new GridLength(5, GridUnitType.Star);

        }


    }
}
