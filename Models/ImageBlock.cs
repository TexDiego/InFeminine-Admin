using InFeminine_Admin.Interfaces;

namespace InFeminine_Admin.Models
{
    public class ImageBlock : IVisualBlock
    {
        public ImageSource? ImageSource { get; set; }
        public Aspect Aspect { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Rotation { get; set; }
        public double Opacity { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double Thickness { get; set; }
        public IShape CornerRadius { get; set; }
        public Color StrokeColor { get; set; } = Colors.Transparent;
        public Color BackGroundColor { get; set; } = Colors.Transparent;
        public LayoutOptions HorizontalOptions { get; set; } = LayoutOptions.Center;
        public Thickness Margin { get; set; } = new Thickness(5, 5, 5, 5);

        public View BuildView()
        {
            var image = new Image
            {
                Source = ImageSource,
                Aspect = Aspect,
                Rotation = Rotation,
                Opacity = Opacity,
                ScaleX = ScaleX,
                ScaleY = ScaleY,
            };

            var container = new Border
            {
                Content = image,
                Padding = 0,
                Stroke = StrokeColor,
                StrokeThickness = Thickness,
                StrokeShape = CornerRadius,
                WidthRequest = Width,
                HeightRequest = Height,
                BackgroundColor = BackGroundColor,
                HorizontalOptions = HorizontalOptions,
                Margin = Margin
            };

            var gesture = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
            gesture.Tapped += async (s, e) =>
            {
                var confirmar = await Application.Current.MainPage.DisplayAlert(
                    "Remover imagem?",
                    "Deseja remover esta imagem?",
                    "Sim", "Cancelar");

                if (confirmar && container.Parent is Layout parent)
                    parent.Children.Remove(container);
            };

            container.GestureRecognizers.Add(gesture);

            return container;
        }
    }

}
