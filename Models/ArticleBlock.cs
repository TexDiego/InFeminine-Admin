using InFeminine_Admin.Interfaces;

namespace InFeminine_Admin.Models
{
    public class ArticleBlock : IVisualBlock
    {
        // ArticleBlock
        public Guid Guid { get; }
        public Action<IVisualBlock>? ActionRequested { get; set; }
        public View? ViewReference { get; set; }


        public List<Article> Pages { get; set; }
        public bool Loop { get; set; }
        public int HeightRequest { get; set; }
        public Thickness Margin { get; set; } 


        public View BuildView()
        {
            var carousel = new CarouselView
            {
                ItemsSource = Pages,
                Loop = Loop,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                
                ItemTemplate = new DataTemplate(() =>
                {
                    var button = new Button() { Margin = 10 };

                    button.SetBinding(Button.TextProperty, "Text");
                    button.SetBinding(Button.CommandProperty, "Command");
                    button.SetBinding(Button.BackgroundColorProperty, "BackgroundColor");
                    button.SetBinding(Button.TextColorProperty, "TextColor");
                    button.SetBinding(Button.FontSizeProperty, "FontSize");
                    button.SetBinding(Button.CornerRadiusProperty, "CornerRadius");
                    button.SetBinding(Button.BorderColorProperty, "BorderColor");
                    button.SetBinding(Button.BorderWidthProperty, "BorderWidth");
                    button.SetBinding(Button.HeightRequestProperty, "HeightRequest");
                    button.SetBinding(Button.IsEnabledProperty, "IsEnabled");
                    button.SetBinding(Button.ImageSourceProperty, "ImageSource");

                    return button;
                })
            };

            var border = new Border
            {
                Content = carousel,
                StrokeThickness = 0,
                BackgroundColor = Colors.Transparent,
                Padding = 10,
                Margin = Margin,
                HeightRequest = HeightRequest + 10
            };

            var actiongesture = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
            actiongesture.Tapped += (s, e) => ActionRequested?.Invoke(this);
            border.GestureRecognizers.Add(actiongesture);

            ViewReference = border;
            return border;
        }

    }
}
