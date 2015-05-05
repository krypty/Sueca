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
        private List<Canvas> _listLaidCardsCanvas;
        private List<Canvas> _listPlayersCanvas;

        public BoardPane()
        {
            InitializeComponent();
            //ResetCards();
            //DrawAllPlayersCards();

            ServiceManager.GetInstance().OnGameInfoUpdated += OnGameInfoUpdated;
            _canPlayerPlay = false;

            _listLaidCardsCanvas = new List<Canvas> { LaidCardSouth, LaidCardWest, LaidCardNorth, LaidCardEast };

            _listPlayersCanvas = new List<Canvas> { EastPlayerCanvas, NorthPlayerCanvas, WestPlayerCanvas };
        }

        private void OnGameInfoUpdated(GameInfo gameInfo)
        {
            _firstCardPlayedThisTurn = gameInfo.FirstCard != null ? CardTools.FromService(gameInfo.FirstCard) : null;

            // update laid cards and players cards
            UpdateOthersPlayersCards(gameInfo.ListPlayer); // player North, West, East
            UpdatePlayerCards(gameInfo.ListCardsPlayer); // player South
            UpdateLaidCards(gameInfo.ListCardsPlayed);

            _canPlayerPlay = gameInfo.IsMyTurn;

            YourTurnLabel.Visibility = _canPlayerPlay ? Visibility.Visible : Visibility.Hidden;

            _playerToken = ServiceManager.GetInstance().PlayerToken;

            UpdateAssetCard(gameInfo.Asset);
        }

        private void UpdateAssetCard(CardColor cardColor)
        {
            if (cardColor == null || cardColor == CardColor.None)
            {
                AssetCardLabel.Content = "-";
            }
            else
            {
                switch (cardColor)
                {
                    case CardColor.Clubs:
                        AssetCardLabel.Content = "Trèfles";
                        break;

                    case CardColor.Diamonds:
                        AssetCardLabel.Content = "Carreaux";
                        break;

                    case CardColor.Hearts:
                        AssetCardLabel.Content = "Coeurs";
                        break;

                    case CardColor.Spades:
                        AssetCardLabel.Content = "Piques";
                        break;
                }
            }
        }

        private void UpdateLaidCards(ServiceReference1.Card[] listCards)
        {
            //foreach (var card in listCards.Where(card => card != null))
            //{
            //    Console.WriteLine("card played: " + card.Value + " of " + card.Color);
            //}

            //Console.Write("list played cards: " + listCards.Length);

            ClearLaidsCardCanvas();

            for (int i = 0; i < listCards.Length; i++)
            {
                ServiceReference1.Card card = listCards[i];
                if (card != null)
                {
                    Console.Write(card.Value + " of " + card.Color + ", ");
                    Card convertedCard = CardTools.FromService(card);
                    ReplaceLaidCard(convertedCard, _listLaidCardsCanvas[i]);
                }
            }
            Console.WriteLine();
        }

        private void ClearLaidsCardCanvas()
        {
            foreach (var canvas in _listLaidCardsCanvas)
            {
                canvas.Children.Clear();
            }
        }

        private void UpdateOthersPlayersCards(Player[] tabPlayers)
        {
            Console.WriteLine("players size: " + tabPlayers.Count());

            for (int i = 0; i < _listPlayersCanvas.Count; i++)
            {
                List<Card> listCards = new List<Card>();

                Console.WriteLine("holding cards: " + tabPlayers[i + 1].HoldingCards);
                for (int j = 0; j < tabPlayers[i + 1].HoldingCards; j++)
                {
                    listCards.Add(CardImageFactory.CreateFaceDownCard());
                }

                DrawAllPlayersCards(_listPlayersCanvas[i], listCards);
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
            playerCanvas.Children.Clear();
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

                playerCanvas.Children.Add(card);

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
