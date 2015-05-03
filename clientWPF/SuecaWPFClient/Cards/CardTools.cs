namespace suecaWPFClient.Cards
{
    public static class CardTools
    {
        public static ServiceReference1.Card ToService(Card card)
        {
            ServiceReference1.Card convertedCard = new ServiceReference1.Card
            {
                Color = card.CardColor,
                Value = card.CardValue
            };

            return convertedCard;
        }

        public static Card FromService(ServiceReference1.Card card)
        {
            return CardImageFactory.CreateCard(card.Color, card.Value);
        }
    }
}
