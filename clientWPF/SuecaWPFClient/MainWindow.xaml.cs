using System.Windows;
using suecaWPFClient.GamePanes;

namespace suecaWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var paneSwitcher = new PaneSwitcher(SideMenuPaneExpander);
            paneSwitcher.BoardEnabled += PaneSwitcherOnBoardEnabled;

            MyBoard.SetBoardEnabled(false);
        }

        private void PaneSwitcherOnBoardEnabled(bool isEnabled)
        {
            MyBoard.SetBoardEnabled(isEnabled);
        }

        private void ExpanderChanged(object sender, RoutedEventArgs e)
        {
            LeftSideColumn.Width = SideMenuPaneExpander.IsExpanded ? new GridLength(3, GridUnitType.Star) : new GridLength(23);
            RightSideColumn.Width = new GridLength(5, GridUnitType.Star);

        }


    }
}
