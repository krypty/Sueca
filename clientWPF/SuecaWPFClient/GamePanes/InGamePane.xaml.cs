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
        }

        private void OnRoomUpdated(Room room)
        {
            foreach (var player in room.listPlayers.Where(player => player.token == ServiceManager.GetInstance().PlayerToken))
            {
                UserPlayerNumber.Content = player.token;
            }

            if (room.RoomState == Room.StateRoom.END_GAME)
            {
                Console.WriteLine("yolo spaghetti");
                ChangeState(GameState.EndGame);
            }
        }

        protected override void Quit()
        {
            throw new System.NotImplementedException();
        }

        private void BtnQuit_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeState(GameState.ListRoom);
        }
    }
}
