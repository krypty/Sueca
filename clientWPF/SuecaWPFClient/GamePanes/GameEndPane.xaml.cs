using System;
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
            //if (room.RoomState == Room.StateRoom.END_GAME)
            //{
            //    foreach (var player in room.listPlayers)
            //    {

            //    }
            //}

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
