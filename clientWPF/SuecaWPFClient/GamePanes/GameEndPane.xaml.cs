using System;
using System.Linq;
using System.Windows;
using suecaWPFClient.ServiceReference1;

namespace suecaWPFClient.GamePanes
{
    /// <summary>
    /// Interaction logic for GameEndPane.xaml
    /// </summary>
    public partial class GameEndPane : GameStatePaneA
    {
        public GameEndPane()
        {
            InitializeComponent();
            ServiceManager.GetInstance().OnRoomUpdated += OnRoomUpdated;
        }

        private void OnRoomUpdated(Room room)
        {
            if (room.listPlayers.Length == 4)
            {
                PlayerSouthScore.Content = room.listPlayers[0].Score;
                PlayerWestScore.Content = room.listPlayers[1].Score;
                PlayerNorthScore.Content = room.listPlayers[2].Score;
                PlayerEastScore.Content = room.listPlayers[3].Score;
            }

            Console.WriteLine("room updated GameEndPane: " + room.RoomState);
        }

        protected override void Quit()
        {
            //throw new System.NotImplementedException();
        }

        private void BtnBackToRoomList_OnClick(object sender, RoutedEventArgs e)
        {
            string playerToken = ServiceManager.GetInstance().PlayerToken;
            ServiceManager.GetInstance().SendEndGameReceived(playerToken);
            ChangeState(GameState.WaitingRoom);
        }
    }
}
