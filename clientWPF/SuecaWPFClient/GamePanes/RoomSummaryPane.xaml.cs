using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using suecaWPFClient.ServiceReference1;

namespace suecaWPFClient.GamePanes
{
    /// <summary>
    /// Interaction logic for RoomSummaryPane.xaml
    /// </summary>
    public partial class RoomSummaryPane : GameStatePaneA
    {
        public RoomSummaryPane()
        {
            InitializeComponent();
            ServiceManager.GetInstance().OnRoomUpdated += OnRoomUpdated;
            ServiceManager.GetInstance().OnGameInfoUpdated += OnGameInfoUpdated;
        }

        private void OnGameInfoUpdated(GameInfo gameInfo)
        {
            Console.WriteLine("roomsummarypane: gameinfo updated");
            ChangeState(GameState.InGame);
        }

        private void OnRoomUpdated(Room room)
        {
            int playerCount = room.listPlayers.Length;
            int playersReady = room.listPlayers.Count(player => player.isReady);

            RoomNameLabel.Content = room.Name;
            PlayerConnectedLabel.Content = playerCount + "/4";
            PlayerReadyLabel.Content = playersReady + "/4";
        }

        protected override void Quit()
        {
            ChangeState(GameState.ListRoom);
        }

        private void SendReadyCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            bool isReady = (sender as CheckBox).IsChecked.Value;
            MessageBox.Show("isReady: " + isReady);
            String playerToken = ServiceManager.GetInstance().PlayerToken;
            ServiceManager.GetInstance().SendReady(playerToken, isReady);
        }
    }
}
