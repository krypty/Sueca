using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using suecaWPFClient.Cards;
using suecaWPFClient.ServiceReference1;
using Card = suecaWPFClient.Cards.Card;

namespace suecaWPFClient
{
    // callback freezes if true
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class BoardPane : UserControl//, SuecaCallback
    {
        //DuplexChannelFactory<ISuecaContract> _channelFactory;
        //ISuecaContract _suecaClient;

        //        private Sueca _suecaClient;

        private const int MaxCard = 10;
        private List<Card> _listCards = new List<Card>();
        private string _playerToken;
        private Card _firstCardPlayerThisTurn;

        public BoardPane()
        {
            InitializeComponent();
            ResetCards();
            DrawAllPlayersCards();

            ServiceManager.GetInstance().OnGameInfoUpdated += OnGameInfoUpdated;
            _playerToken = ServiceManager.GetInstance().PlayerToken;
        }

        private void OnGameInfoUpdated(GameInfo gameInfo)
        {
            Console.WriteLine("MainWindow: gameinfo" + gameInfo.ToString());
            _firstCardPlayerThisTurn = CardTools.FromService(gameInfo.ListCardsPlayed[0]);

            //TODO: populate _listCard with cards from gameInfo
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

        public void SetBoardEnabled(bool enabled)
        {
            IsEnabled = enabled;
            DisablingRectangle.Visibility = enabled ? Visibility.Collapsed : Visibility.Visible;
        }

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
            Console.WriteLine(card.ToString());

            bool removed = _listCards.Remove(card);
            SouthPlayerCanvas.Children.Remove(card);
            ReplaceLaidCard(card);
            Console.WriteLine("card removed ? " + removed);
            DrawAllPlayersCards();

            if (IsCardValid(card))
            {
                ServiceManager.GetInstance().PlayCard(_playerToken, card);
            }

        }

        private bool IsCardValid(Card card)
        {
            return (card.CardColor == _firstCardPlayerThisTurn.CardColor || !_listCards.Exists(c => c.CardColor == _firstCardPlayerThisTurn.CardColor));
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
