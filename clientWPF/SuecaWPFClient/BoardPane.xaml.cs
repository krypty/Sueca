using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using suecaWPFClient.Cards;

namespace suecaWPFClient
{
    // callback freezes if true
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class BoardPane: UserControl//, SuecaCallback
    {
        //DuplexChannelFactory<ISuecaContract> _channelFactory;
        //ISuecaContract _suecaClient;

        //        private Sueca _suecaClient;

        private const int MaxCard = 10;
        private List<Card> _listCards = new List<Card>();

        public BoardPane()
        {
            InitializeComponent();
            //            _suecaClient = new SuecaClient(new InstanceContext(this));
            ResetCards();
            DrawAllPlayersCards();

            ServiceManager.GetInstance().OnGameInfoUpdated += ServiceManagerOnOnGameInfoUpdated;
        }

        private void ServiceManagerOnOnGameInfoUpdated(string message)
        {
            MessageBox.Show("MainWindow: " + message);
        }

        private void ResetCards()
        {
            _listCards.Clear();
            SouthPlayerCanvas.Children.Clear();
            for (int i = 0; i < MaxCard; i++)
            {
                _listCards.Add(CardImageFactory.CreateRandomCard());
            }
            AddCardsInGameBoard();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // create instance
            //_channelFactory = new DuplexChannelFactory<ISuecaContract>(this, "configClient");
            //_suecaClient = _channelFactory.CreateChannel();
            Console.WriteLine("Client started");

            // create room
            Console.WriteLine("create room...");
            //String roomName = _suecaClient.CreateRoom("no_password");
            String roomName = ServiceManager.GetInstance().CreateRoom("no_password");
            MessageBox.Show("roomName: " + roomName);

            // join it 
            Console.WriteLine("Join room");
            //String playerToken = _suecaClient.JoinRoom(roomName, "other_password_that_fail");
            String playerToken = ServiceManager.GetInstance().JoinRoom(roomName, "other_password_that_fail");
            Console.WriteLine("[client] playerToken: " + playerToken);
        }

        private void btnListRoom_Click(object sender, RoutedEventArgs e)
        {
            //            var listRoom = _suecaClient.ListRoom();
            var listRoom = ServiceManager.GetInstance().ListRoom();

            StringBuilder builder = new StringBuilder();

            foreach (var room in listRoom)
            {
                builder.AppendLine(room.ToString());
            }

            MessageBox.Show(builder.ToString());
        }

        //// callback
        //public void GameStarted(string message)
        //{
        //    Console.WriteLine("[Client] server says: " + message);
        //}

        private void Ellipse_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetCards();
            DrawAllPlayersCards();
        }

        private void AddCardsInGameBoard()
        {
            foreach (Card card in _listCards)
            {
                card.MouseDown += Card_OnMouseDown;
                card.MouseEnter += CardOnMouseEnter;
                card.MouseLeave += CardOnMouseLeave;
                SouthPlayerCanvas.Children.Add(card);
            }
        }

        private void CardOnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            Card card = (Card)sender;
            HighlightCard(card, true);
        }

        private void CardOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            Card card = (Card)sender;
            HighlightCard(card, false);
        }

        private void DrawAllPlayersCards()
        {
            double canvasWidth = SouthPlayerCanvas.Width;
            double cardWidth = (1.0 / 12.0) * canvasWidth;

            double x = 0;
            double offset = (3.0 / 4.0) * cardWidth + ((MaxCard - _listCards.Count) / 2.0) * cardWidth;
            for (int i = 0; i < _listCards.Count; i++)
            {
                if (i == 0) x += offset;

                Card card = _listCards[i];
                card.SetValue(Canvas.LeftProperty, x);
                card.SetValue(Canvas.TopProperty, 0.0);


                //Card playerNorthCard = CardImageFactory.CreateRandomCard();
                //playerNorthCard.MouseDown += Card_OnMouseDown;
                //playerNorthCard.SetValue(Canvas.LeftProperty, x);
                //playerNorthCard.SetValue(Canvas.TopProperty, 0.0);
                //NorthPlayerCanvas.Children.Add(playerNorthCard);


                //Card playerWestCard = CardImageFactory.CreateRandomCard();
                //playerWestCard.MouseDown += Card_OnMouseDown;
                //playerWestCard.SetValue(Canvas.LeftProperty, x);
                //playerWestCard.SetValue(Canvas.TopProperty, 0.0);
                //WestPlayerCanvas.Children.Add(playerWestCard);

                //Card playerEastCard = CardImageFactory.CreateRandomCard();
                //playerEastCard.MouseDown += Card_OnMouseDown;
                //playerEastCard.SetValue(Canvas.LeftProperty, x);
                //playerEastCard.SetValue(Canvas.TopProperty, 0.0);
                //EastPlayerCanvas.Children.Add(playerEastCard);

                x += cardWidth;
            }
        }



        private static void HighlightCard(Card card, bool isHighlighted)
        {
            const double scaling = 2.5;
            if (isHighlighted)
            {
                card.Width = card.Width * scaling;
                card.Height = card.Height * scaling;
                card.SetValue(Canvas.TopProperty, -100.0);
                card.SetValue(Panel.ZIndexProperty, 100);
            }
            else
            {
                card.Width = card.Width / scaling;
                card.Height = card.Height / scaling;
                card.SetValue(Canvas.TopProperty, 0.0);
                card.SetValue(Panel.ZIndexProperty, card.OriginalZIndex);
            }
        }

        private void Card_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("hello from custom card !");
            Card card = (Card)sender;
            MessageBox.Show(card.ToString());

            bool removed = _listCards.Remove(card);
            SouthPlayerCanvas.Children.Remove(card);
            ReplaceLaidCard(card);
            Console.WriteLine("card removed ? " + removed);
            DrawAllPlayersCards();
        }

        private void ReplaceLaidCard(Card card)
        {
            card.MouseDown -= Card_OnMouseDown;
            card.MouseEnter -= CardOnMouseEnter;
            card.MouseLeave -= CardOnMouseLeave;
            card.SetValue(Canvas.LeftProperty, 0.0);
            card.SetValue(Canvas.TopProperty, 0.0);

            LaidCardSouth.Children.Clear();
            LaidCardSouth.Children.Add(card);
        }
    }
}
