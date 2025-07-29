using InFeminine_Admin.Interfaces;
using Microsoft.Maui.Graphics.Text;

namespace InFeminine_Admin.Models
{
    public class TextBlock : IVisualBlock
    {
        public string Text { get; set; } = string.Empty;
        public double Size { get; set; } = 12;
        public FontAttributes Attributes { get; set; } = FontAttributes.None;
        public TextAlignment Alignment { get; set; } = TextAlignment.Start;
        public Thickness Margin { get; set; } = new Thickness(5, 5, 5, 5);
        public Color TextColor { get; set; } = Colors.Black;

        public View BuildView()
        {
            var label = new Label
            {
                Text = Text,
                FontSize = Size,
                FontAttributes = Attributes,
                HorizontalTextAlignment = Alignment,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = TextColor
            };

            var container = new Border
            {
                Content = label,
                Margin = Margin,
                Padding = 0,
                Background = Colors.Transparent,
                Stroke = Colors.Transparent,
                StrokeThickness = 0
            };

            var doubleTap = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
            doubleTap.Tapped += async (s, e) =>
            {
                var confirmar = await Application.Current.MainPage.DisplayAlert(
                    "Remover",
                    "Deseja remover este texto?",
                    "Sim",
                    "Cancelar");

                if (confirmar && container.Parent is Layout parent)
                    parent.Children.Remove(container);
            };

            container.GestureRecognizers.Add(doubleTap);

            return container;
        }
    }
}
