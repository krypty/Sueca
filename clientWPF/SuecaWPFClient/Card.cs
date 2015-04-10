using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace suecaWPFClient
{
    public class Card : Control
    {
        public Card()
        {
            Height = 279;
            Width = 200;
        }

        public CardColor CardColor { get; private set; }
        public CardValue CardValue { get; private set; }

        private readonly String _imagePath;

        public Card(CardColor cardColor, CardValue cardValue, String imagePath) : this()
        {
            CardColor = cardColor;
            CardValue = cardValue;
            _imagePath = imagePath;
        }
            

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(null, new Pen(Brushes.Black, 2), new Rect(0, 0, 150, 220));
            var image = new BitmapImage(new Uri(_imagePath));
            drawingContext.DrawImage(image, new Rect(new Size(Width, Height)));
        }
    }
}
