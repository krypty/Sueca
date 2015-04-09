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
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class MainWindow : Window, SuecaCallback
    {

        private Sueca _suecaClient;
        private string myToken, roomName, password;

        public MainWindow()
        {
            InitializeComponent();

            _suecaClient = new SuecaClient(new InstanceContext(this));

        }



        public void GameStarted(string message)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                labelCallback.Content = message;

            }));
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Client started");

            password = textboxPassword.Text;
            // create room
            Console.WriteLine("create room...");
            roomName = _suecaClient.CreateRoom(password);

            textboxName.Text = roomName;
            MessageBox.Show("roomName: " + roomName);
        }

        private void Join_Click(object sender, RoutedEventArgs e)
        {
            roomName = textboxName.Text;
            password = textboxPassword.Text;
            // join it 
            Console.WriteLine("Join room");
            myToken = _suecaClient.JoinRoom(roomName, password);
            Console.WriteLine("[client] playerToken: " + myToken);
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            _suecaClient.SendReady(myToken,true);
        }

        private void List_Click(object sender, RoutedEventArgs e)
        {
            var roomList = _suecaClient.ListRoom();

            listRoom.Items.Clear();

            foreach (var room in roomList)
            {
                listRoom.Items.Add(room.Name);
            }

        }
    }
}
