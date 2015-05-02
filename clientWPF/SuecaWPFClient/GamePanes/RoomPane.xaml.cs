using System;
using System.Windows;
using System.Windows.Input;
using suecaWPFClient.ServiceReference1;

namespace suecaWPFClient.GamePanes
{
    /// <summary>
    /// Interaction logic for RoomPane.xaml
    /// </summary>
    public partial class RoomPane : GameStatePaneA
    {

        private Room[] _rooms;

        public RoomPane()
        {
            InitializeComponent();
            ServiceManager.GetInstance().OnGameInfoUpdated += GameInfoUpdated;
        }

        private void BtnCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            CreateRoomWindow roomWindow = new CreateRoomWindow();

            string roomPassword = "";
            if (roomWindow.ShowDialog() == true)
            {
                roomPassword = roomWindow.RoomPassword;
            }

            Console.WriteLine("pass: " + roomPassword);

            var roomName = ServiceManager.GetInstance().CreateRoom(roomPassword);
            MessageBox.Show("Une salle a été créée avec le nom: " + roomName);
            RefreshRoomList();
        }

        private void GameInfoUpdated(GameInfo gameInfo)
        {
            Console.WriteLine("RoomPane: gameinfo: " + gameInfo.ToString());
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshRoomList();
        }

        private void RefreshRoomList()
        {
            RoomListView.Items.Clear();
            _rooms = ServiceManager.GetInstance().ListRoom();

            foreach (var room in _rooms)
            {
                this.RoomListView.Items.Add(room);
            }

        }

        private void ListViewClick(object sender, MouseButtonEventArgs e)
        {
            JoinRoom();
        }

        private void BtnConnect_OnClick(object sender, RoutedEventArgs e)
        {
            JoinRoom();
        }

        private void JoinRoom()
        {
            Room room = (Room)RoomListView.SelectedItem;
            if (room == null) return;

            Console.WriteLine("joinRoom");

            string roomPassword = "";
            CreateRoomWindow roomWindow = new CreateRoomWindow();
            if (roomWindow.ShowDialog() == true)
            {
                roomPassword = roomWindow.RoomPassword;
            }

            Console.WriteLine("pass: " + roomPassword);
            var playerID = ServiceManager.GetInstance().JoinRoom(room.Name, roomPassword);
            ServiceManager.GetInstance().PlayerToken = playerID;

            if (playerID == null)
            {
                MessageBox.Show("Mot de passe invalide, essayez encore");
                return;
            }

            Console.WriteLine("Room joined. PlayerID: " + playerID);

            // switch the view to waiting room
            ChangeState(GameState.WaitingRoom);
        }

        protected override void Quit()
        {
            //TODO: see if it's necessary to stop listening to callback ?
            ChangeState(GameState.ListRoom);
        }
    }
}
