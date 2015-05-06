using System;
using System.Linq;
using System.Windows;
using suecaWPFClient.ServiceReference1;

namespace suecaWPFClient.GamePanes
{
    /// <summary>
    /// Interaction logic for GameSummaryPane.xaml
    /// </summary>
    public partial class InGamePane : GameStatePaneA
    {
        public InGamePane()
        {
            InitializeComponent();

            ServiceManager.GetInstance().OnRoomUpdated += OnRoomUpdated;
            ServiceManager.GetInstance().OnGameInfoUpdated += OnGameInfoUpdated;
        }

        private void OnGameInfoUpdated(GameInfo gameInfo)
        {
            var listPlayer = gameInfo.ListPlayer;
            if (listPlayer.Length != 4) return;

            PlayerSouthScore.Content = listPlayer[0].Score;
            PlayerWestScore.Content = listPlayer[1].Score;
            PlayerNorthScore.Content = listPlayer[2].Score;
            PlayerEastScore.Content = listPlayer[3].Score;
        }

        private void OnRoomUpdated(Room room)
        {
            var player = room.listPlayers.FirstOrDefault(p => p.token == ServiceManager.GetInstance().PlayerToken);

            if (player != null)
                Console.WriteLine("player token: " + player.token);


            if (room.RoomState == Room.StateRoom.END_GAME)
            {
                ChangeState(GameState.EndGame);
            }
        }

        protected override void Quit()
        {
            //TODO..
        }

        private void BtnQuit_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeState(GameState.ListRoom);
        }
    }
}
