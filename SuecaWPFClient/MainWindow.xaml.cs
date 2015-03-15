using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Windows;
using SuecaContracts;

namespace suecaWPFClient
{
    // callback freezes if true
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class MainWindow : Window, ISuecaCallbackContract
    {
        DuplexChannelFactory<ISuecaContract> _channelFactory;
        ISuecaContract _suecaClient;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // create instance
            _channelFactory = new DuplexChannelFactory<ISuecaContract>(this, "configClient");
            _suecaClient = _channelFactory.CreateChannel();
            Console.WriteLine("Client started");

            // create room
            Console.WriteLine("create room...");
            String roomName = _suecaClient.CreateRoom("no_password");
            MessageBox.Show("roomName: " + roomName);

            // join it 
            Console.WriteLine("Join room");
            String playerToken = _suecaClient.JoinRoom(roomName, "other_password_that_fail");
            Console.WriteLine("[client] playerToken: " + playerToken);
        }



        private void btnListRoom_Click(object sender, RoutedEventArgs e)
        {
            List<Room> listRoom = _suecaClient.ListRoom();

            StringBuilder builder = new StringBuilder();

            foreach (var room in listRoom)
            {
                builder.AppendLine(room.ToString());
            }

            MessageBox.Show(builder.ToString());
        }

        // callback
        public void GameStarted(string message)
        {
            MessageBox.Show("CALLBACK: \n" + message);
            Console.WriteLine("[Client] server says: " + message);
        }
    }
}
