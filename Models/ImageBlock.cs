using InFeminine_Admin.Interfaces;
using InFeminine_Admin.ViewModels;
using InFeminine_Admin.Views;
using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;

namespace InFeminine_Admin.Models
{
    public class ImageBlock : IVisualBlock
    {

        // ImageBlock
        public Guid Guid { get; } = Guid.NewGuid();
        public Action<IVisualBlock>? ActionRequested { get; set; }
        public View? ViewReference { get; set; }


        // Image properties
        public ImageSource? ImageSource { get; set; }
        public Aspect Aspect { get; set; } = Aspect.AspectFit;
        public double Width { get; set; } = 100;
        public double Height { get; set; } = 100;
        public int Rotation { get; set; } = 0;
        public double Opacity { get; set; } = 1;
        public double ScaleX { get; set; } = 1;
        public double ScaleY { get; set; } = 1;

        // Border properties
        public double Thickness { get; set; } = 0;
        public IShape CornerRadius { get; set; } = new RoundRectangle { CornerRadius = new CornerRadius(0) };
        public Color StrokeColor { get; set; } = Colors.Transparent;
        public Color BackGroundColor { get; set; } = Colors.Transparent;
        public LayoutOptions HorizontalOptions { get; set; } = LayoutOptions.Center;
        public Thickness Margin { get; set; } = new Thickness(5, 5, 5, 5);
        public Thickness Padding { get; set; } = 0;

        // Shadow properties
        public float ShadowRadius { get; set; } = 0f;
        public Color ShadowColor { get; set; } = Colors.Transparent;
        public float ShadowOpacity { get; set; } = 0f;
        public Point ShadowOffset { get; set; } = new Point(0, 0);

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
                Padding = Padding,
                Stroke = StrokeColor,
                StrokeThickness = Thickness,
                StrokeShape = CornerRadius,
                WidthRequest = Width,
                HeightRequest = Height,
                BackgroundColor = BackGroundColor,
                HorizontalOptions = HorizontalOptions,
                Margin = Margin,

                Shadow = new Shadow
                {
                    Radius = ShadowRadius,
                    Brush = ShadowColor,
                    Opacity = ShadowOpacity,
                    Offset = ShadowOffset
                }
            };

            var actiongesture = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
            actiongesture.Tapped += (s, e) =>
            {
                Debug.WriteLine("Action called");
                ActionRequested?.Invoke(this);
            };


            container.GestureRecognizers.Add(actiongesture);
            ViewReference = container;
            container.BindingContext = this;

            return container;
        }
    }

}
