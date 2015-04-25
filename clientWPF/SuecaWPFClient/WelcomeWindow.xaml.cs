using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using suecaWPFClient.ServiceReference1;

namespace suecaWPFClient
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    
    public partial class WelcomeWindow : Window
    {
        private Room[] _rooms;

        public WelcomeWindow()
        {
            InitializeComponent();
            ServiceManager.GetInstance().OnGameInfoUpdated += GameInfoUpdated;
            //RefreshRoomList(); //TODO: async
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

        private void GameInfoUpdated(string message)
        {
            MessageBox.Show("message: " + message);
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

            if (playerID == null)
            {
                MessageBox.Show("Invalid password");
            }
            else
            {
                MessageBox.Show("Room joined. PlayerID: " + playerID);
            }
            
        }
    }
}
