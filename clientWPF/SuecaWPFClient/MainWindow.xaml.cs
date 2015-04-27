using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace suecaWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int i = 0; // TODO: remove me

        private RoomsPane RoomsPane = new RoomsPane();
        private RoomSummaryPane RoomSummaryPane = new RoomSummaryPane();
        private GameEndPane GameEndPane = new GameEndPane();
        private GameSummaryPane GameSummaryPane = new GameSummaryPane();

        private readonly List<UserControl> _listUserControls = new List<UserControl>();

        public MainWindow()
        {
            InitializeComponent();
            AddPanes();
        }

        private void AddPanes()
        {
            _listUserControls.Add(RoomsPane);
            _listUserControls.Add(RoomSummaryPane);
            _listUserControls.Add(GameEndPane);
            _listUserControls.Add(GameSummaryPane);
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: remove me
            Expander.Content = _listUserControls[++i % 4];
        }

        private void ExpanderChanged(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Expander.IsExpanded);
            LeftSideColumn.Width = Expander.IsExpanded ? new GridLength(3, GridUnitType.Star) : new GridLength(23);
            RightSideColumn.Width = new GridLength(5, GridUnitType.Star);

        }
    }
}
