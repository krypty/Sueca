using System;
using System.Text;
using suecaWPFClient.ServiceReference1;

namespace suecaWPFClient.Cards
{
    class CardImageFactory
    {
        //static
        private static readonly Random random = new Random();

        //members
        private const String RootImagesPath = @"pack://application:,,,/Images/Cartes/";
        
        public static Card CreateRandomCard()
        {
            Array values = Enum.GetValues(typeof(CardValue));
            CardValue randomCardValue;
            do
            {
                randomCardValue = (CardValue)values.GetValue(random.Next(values.Length));
            } while (randomCardValue.Equals(CardValue.None));

            return CreateCard(CardColor.Hearts, randomCardValue);
        }

        public static Card CreateFaceDownCard()
        {
            return new Card(CardColor.None, CardValue.None, RootImagesPath + "DosCarte.png");
        }

        public static Card CreateCard(CardColor cardColor, CardValue cardValue)
        {
            if (cardColor.Equals(CardColor.None) || cardValue.Equals(CardValue.None))
            {
                throw new ArgumentException("Impossible to create face down card here. Please use CreateFaceDownCard() method.");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(RootImagesPath);
            builder.Append(cardColor);
            builder.Append(@"\");

            switch (cardValue)
            {
                case CardValue.Two:
                    builder.Append("2.png");
                    break;

                case CardValue.Three:
                    builder.Append("3.png");
                    break;

                case CardValue.Four:
                    builder.Append("4.png");
                    break;

                case CardValue.Five:
                    builder.Append("5.png");
                    break;

                case CardValue.Six:
                    builder.Append("6.png");
                    break;

                case CardValue.Seven:
                    builder.Append("7.png");
                    break;

                case CardValue.Jack:
                    builder.Append("J.png");
                    break;

                case CardValue.Queen:
                    builder.Append("Q.png");
                    break;

                case CardValue.King:
                    builder.Append("K.png");
                    break;

                case CardValue.Ace:
                    builder.Append("As.png");
                    break;
            }

            String imagePath = builder.ToString();
            return new Card(cardColor, cardValue, imagePath);
        }
    }
}
