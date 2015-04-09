using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.ServiceModel;
using ClientTest.ServiceReference1;

namespace ClientTest
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, SuecaCallback
    {


        private Sueca _suecaClient;
        private string myToken;

        public MainWindow()
        {
            InitializeComponent();

            _suecaClient = new SuecaClient(new InstanceContext(this));
        }


        public void GameStarted(string message)
        {
            MessageBox.Show("CALLBACK: \n" + message);
            Console.WriteLine("[Client] server says: " + message);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            // create instance
            //_channelFactory = new DuplexChannelFactory<ISuecaContract>(this, "configClient");
            //_suecaClient = _channelFactory.CreateChannel();
            Console.WriteLine("Client started");

            // create room
            Console.WriteLine("create room...");
            String roomName = _suecaClient.CreateRoom("no_password");
            MessageBox.Show("roomName: " + roomName);

            // join it 
            Console.WriteLine("Join room");
            myToken = _suecaClient.JoinRoom(roomName, "no_password");
            Console.WriteLine("[client] playerToken: " + myToken);
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            _suecaClient.SendReady(myToken,true);
        }

        private void List_Click(object sender, RoutedEventArgs e)
        {
            var listRoom = _suecaClient.ListRoom();

            StringBuilder builder = new StringBuilder();

            foreach (var room in listRoom)
            {
                builder.AppendLine(room.ToString());
            }

            MessageBox.Show(builder.ToString());
        }
    }
}
