using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
        private Card _firstCardPlayedThisTurn;

        private bool _canPlayerPlay;
        private List<Canvas> _listCanvas;

        public BoardPane()
        {
            InitializeComponent();
            //ResetCards();
            //DrawAllPlayersCards();

            ServiceManager.GetInstance().OnGameInfoUpdated += OnGameInfoUpdated;
            _canPlayerPlay = false;

            _listCanvas = new List<Canvas>();
            _listCanvas.Add(LaidCardSouth);
            _listCanvas.Add(LaidCardWest);
            _listCanvas.Add(LaidCardNorth);
            _listCanvas.Add(LaidCardEast);
        }

        private void OnGameInfoUpdated(GameInfo gameInfo)
        {
            _firstCardPlayedThisTurn = gameInfo.FirstCard != null ? CardTools.FromService(gameInfo.FirstCard) : null;

            // update laid cards and players cards
            UpdateOthersPlayersCards(gameInfo.ListPlayer); // player North, West, East
            UpdatePlayerCards(gameInfo.ListCardsPlayer); // player South
            UpdateLaidCards(gameInfo.ListCardsPlayed);

            _canPlayerPlay = gameInfo.NumberPlayerToPlay == gameInfo.Player.NumberTurn;

            YourTurnLabel.Visibility = _canPlayerPlay ? Visibility.Visible : Visibility.Hidden;

            _playerToken = ServiceManager.GetInstance().PlayerToken;
        }

        private void UpdateLaidCards(ServiceReference1.Card[] listCards)
        {
            //throw new NotImplementedException();

            Console.Write("list played cards: ");
            int i = 0;

            ClearLaidsCardCanvas();
            foreach (var card in listCards)
            {
                Console.Write(card.Value + " of " + card.Color + ", ");
                Card convertedCard = CardTools.FromService(card);
                ReplaceLaidCard(convertedCard, _listCanvas[i]);

                i++;
            }
            Console.WriteLine();
        }

        private void ClearLaidsCardCanvas()
        {
            foreach (var canvas in _listCanvas)
            {
                canvas.Children.Clear();
            }
        }

        private void UpdateOthersPlayersCards(IEnumerable<Player> players)
        {
            Console.WriteLine("players size: " + players.ToArray().Count());
            List<Canvas> listCanvas = new List<Canvas>();
            listCanvas.Add(NorthPlayerCanvas);
            listCanvas.Add(EastPlayerCanvas);
            listCanvas.Add(WestPlayerCanvas);

            Player[] tabPlayers = players.ToArray();

            for (int i = 0; i < listCanvas.Count; i++)
            {
                List<Card> listCards = new List<Card>();

                Console.WriteLine("holding cards: " + tabPlayers[i].holdingCards);
                for (int j = 0; j < tabPlayers[i].holdingCards; j++)
                {
                    listCards.Add(CardImageFactory.CreateFaceDownCard());
                }

                DrawAllPlayersCards(listCanvas[i], listCards);
            }

        }

        private void UpdatePlayerCards(ServiceReference1.Card[] listCardsPlayer)
        {
            _listCards.Clear();
            SouthPlayerCanvas.Children.Clear();
            foreach (var card in listCardsPlayer)
            {
                _listCards.Add(CardTools.FromService(card));
            }
            AddCardsInGameBoard();

            DrawAllPlayersCards(SouthPlayerCanvas, _listCards);
        }

        //private void ResetCards()
        //{
        //    _listCards.Clear();
        //    SouthPlayerCanvas.Children.Clear();
        //    for (int i = 0; i < MaxCard; i++)
        //    {
        //        _listCards.Add(CardImageFactory.CreateRandomCard());
        //    }
        //    AddCardsInGameBoard();
        //}

        public void SetBoardEnabled(bool enabled)
        {
            IsEnabled = enabled;
            DisablingRectangle.Visibility = enabled ? Visibility.Collapsed : Visibility.Visible;
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

        private void DrawAllPlayersCards(Canvas playerCanvas, List<Card> listCards)
        {
            double canvasWidth = playerCanvas.Width;
            double cardWidth = (1.0 / 12.0) * canvasWidth;

            double x = 0;
            double offset = (3.0 / 4.0) * cardWidth + ((MaxCard - listCards.Count) / 2.0) * cardWidth;
            for (int i = 0; i < listCards.Count; i++)
            {
                if (i == 0) x += offset;

                Card card = listCards[i];
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
            Card card = (Card)sender;
            //Console.WriteLine(card.ToString());

            //bool removed = _listCards.Remove(card);
            //SouthPlayerCanvas.Children.Remove(card);
            //ReplaceLaidCard(card);
            //Console.WriteLine("card removed ? " + removed);
            //DrawAllPlayersCards();

            if (!_canPlayerPlay) return;

            if (_firstCardPlayedThisTurn == null || IsCardValid(card))
            {
                ServiceManager.GetInstance().PlayCard(_playerToken, card);
            }
        }

        private bool IsCardValid(Card card)
        {
            return (card.CardColor == _firstCardPlayedThisTurn.CardColor || !_listCards.Exists(c => c.CardColor == _firstCardPlayedThisTurn.CardColor));
        }

        private void ReplaceLaidCard(Card card, Canvas canvas)
        {
            card.MouseDown -= Card_OnMouseDown;
            card.MouseEnter -= CardOnMouseEnter;
            card.MouseLeave -= CardOnMouseLeave;
            card.SetValue(Canvas.LeftProperty, 0.0);
            card.SetValue(Canvas.TopProperty, 0.0);

            canvas.Children.Clear();
            canvas.Children.Add(card);
        }
    }
}
