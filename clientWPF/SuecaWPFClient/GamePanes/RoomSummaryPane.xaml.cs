using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using suecaWPFClient.ServiceReference1;
using CheckBox = System.Windows.Controls.CheckBox;
using MessageBox = System.Windows.MessageBox;

namespace suecaWPFClient.GamePanes
{
    /// <summary>
    /// Interaction logic for RoomSummaryPane.xaml
    /// </summary>
    public partial class RoomSummaryPane : GameStatePaneA
    {
        private Room.StateRoom _lastRoomState;

        public RoomSummaryPane()
        {
            InitializeComponent();
            ServiceManager.GetInstance().OnRoomUpdated += OnRoomUpdated;
            ServiceManager.GetInstance().OnGameInfoUpdated += OnGameInfoUpdated;
            _lastRoomState = Room.StateRoom.WAITING_READY;
        }

        private void OnGameInfoUpdated(GameInfo gameInfo)
        {
            ResetPane();
            //if (_lastRoomState == Room.StateRoom.GAME_IN_PROGRESS)
            //{
            ChangeState(GameState.InGame);
            //}
        }

        private void ResetPane()
        {


            Thread th = new Thread(new ThreadStart(delegate
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    PlayerConnectedLabel.Content = "-/4";
                    PlayerReadyLabel.Content = "-/4";
                    SendReadyCheckBox.IsChecked = false;
                }));

            }));

            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void OnRoomUpdated(Room room)
        {
            int playerCount = room.listPlayers.Length;
            int playersReady = room.listPlayers.Count(player => player.isReady);

            RoomNameLabel.Content = room.Name;
            PlayerConnectedLabel.Content = playerCount + "/4";
            PlayerReadyLabel.Content = playersReady + "/4";

            _lastRoomState = room.RoomState;
        }

        protected override void Quit()
        {
            ChangeState(GameState.ListRoom);
        }

        private void SendReadyCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            bool isReady = (sender as CheckBox).IsChecked.Value;
            String playerToken = ServiceManager.GetInstance().PlayerToken;
            ServiceManager.GetInstance().SendReady(playerToken, isReady);
        }
    }
}
