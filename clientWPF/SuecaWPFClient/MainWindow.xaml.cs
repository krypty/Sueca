using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using suecaWPFClient.ServiceReference1;

namespace suecaWPFClient
{
    // callback freezes if true
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class MainWindow : Window, SuecaCallback
    {
        //DuplexChannelFactory<ISuecaContract> _channelFactory;
        //ISuecaContract _suecaClient;

        private Sueca _suecaClient;


        public MainWindow()
        {
            InitializeComponent();
            _suecaClient = new SuecaClient(new InstanceContext(this));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
            String playerToken = _suecaClient.JoinRoom(roomName, "other_password_that_fail");
            Console.WriteLine("[client] playerToken: " + playerToken);
        }

        private void btnListRoom_Click(object sender, RoutedEventArgs e)
        {
            var listRoom = _suecaClient.ListRoom();

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

        private void Ellipse_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DrawAllPlayersCards();
        }

        private void DrawAllPlayersCards()
        {
            int numberOfCards = 4;
            double canvasWidth = SouthPlayerCanvas.Width;
            double cardWidth = (1.0 / 12.0) * canvasWidth;

            const int maxCard = 10;
            double x = 0;
            double offset = (3.0 / 4.0) * cardWidth + ((maxCard - numberOfCards) / 2.0) * cardWidth;
            for (int i = 0; i < numberOfCards; i++)
            {
                if (i == 0) x += offset;

                Card playerSouthCard = CardImageFactory.CreateRandomCard();
                playerSouthCard.MouseDown += MonShape_OnMouseDown;
                playerSouthCard.MouseEnter += delegate(object sender, MouseEventArgs args) { highlightCard(sender, args, true); };
                playerSouthCard.MouseLeave += delegate(object sender, MouseEventArgs args) { highlightCard(sender, args, false); };
                playerSouthCard.SetValue(Canvas.LeftProperty, x);
                playerSouthCard.SetValue(Canvas.TopProperty, 0.0);
                SouthPlayerCanvas.Children.Add(playerSouthCard);

                Card playerNorthCard = CardImageFactory.CreateRandomCard();
                playerNorthCard.MouseDown += MonShape_OnMouseDown;
                playerNorthCard.SetValue(Canvas.LeftProperty, x);
                playerNorthCard.SetValue(Canvas.TopProperty, 0.0);
                NorthPlayerCanvas.Children.Add(playerNorthCard);


                Card playerWestCard = CardImageFactory.CreateRandomCard();
                playerWestCard.MouseDown += MonShape_OnMouseDown;
                playerWestCard.SetValue(Canvas.LeftProperty, x);
                playerWestCard.SetValue(Canvas.TopProperty, 0.0);
                WestPlayerCanvas.Children.Add(playerWestCard);

                Card playerEastCard = CardImageFactory.CreateRandomCard();
                playerEastCard.MouseDown += MonShape_OnMouseDown;
                playerEastCard.SetValue(Canvas.LeftProperty, x);
                playerEastCard.SetValue(Canvas.TopProperty, 0.0);
                EastPlayerCanvas.Children.Add(playerEastCard);

                x += cardWidth;
            }
        }



        private void highlightCard(object sender, MouseEventArgs args, bool isHighlighted)
        {
            const double scaling = 2.0;
            Card card = sender as Card;
            if (isHighlighted)
            {
                card.Width = card.Width * scaling;
                card.Height = card.Height * scaling;

                card.SetValue(Canvas.TopProperty, -50.0);
            }
            else
            {
                card.Width = card.Width / scaling;
                card.Height = card.Height / scaling;

                card.SetValue(Canvas.TopProperty, 0.0);
            }
        }

        private void MonShape_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("hello from custom card !");
            Card card = sender as Card;

            if (card == null)
            {
                Console.WriteLine("card is null");
                return;
            }
            MessageBox.Show(card.CardValue.ToString());
        }
    }
}
