using InFeminine_Admin.Interfaces;
using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;

namespace InFeminine_Admin.Models
{
    public class TextBlock : IVisualBlock
    {
        public Guid Guid { get; } = Guid.NewGuid();
        public Action<IVisualBlock>? ActionRequested { get; set; }
        public View? ViewReference { get; set; }

        // Text properties
        public string Text { get; set; } = string.Empty;
        public double Size { get; set; } = 12;
        public FontAttributes Attributes { get; set; } = FontAttributes.None;
        public TextDecorations Decorations { get; set; } = TextDecorations.None;
        public TextAlignment Alignment { get; set; } = TextAlignment.Start;
        public Color TextColor { get; set; } = Colors.Black;

        // Border properties
        public LayoutOptions HorizontalAlignment { get; set; } = LayoutOptions.Center;
        public Thickness Margin { get; set; } = new Thickness(5, 5, 5, 5);
        public Thickness Padding { get; set; } = new Thickness(0, 0, 0, 0);
        public Color BackGroundColor { get; set; } = Colors.White;
        public Color BorderColor { get; set; } = Colors.Transparent;
        public double BorderThickness { get; set; } = 0;
        public IShape StrokeShape { get; set; } = new RoundRectangle { CornerRadius = new CornerRadius(0) };


        // Shadow properties
        public float ShadowRadius { get; set; } = 0f;
        public Color ShadowColor { get; set; } = Colors.Transparent;
        public float ShadowOpacity { get; set; } = 0f;
        public Point ShadowOffset { get; set; } = new Point(0, 0);


        // Link property
        public string Link { get; set; } = string.Empty;
        public bool IsLink { get; set; } = false;


        public View BuildView()
        {
            var label = new Label
            {
                Text = Text,
                FontSize = Size,
                FontAttributes = Attributes,
                TextDecorations = Decorations,
                HorizontalTextAlignment = Alignment,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = TextColor
            };

            var container = new Border
            {
                Content = label,
                Margin = Margin,
                Padding = new Thickness { Bottom = 10, Left = 0, Right = 0, Top = 0},
                BackgroundColor = BackGroundColor,
                Stroke = BorderColor,
                StrokeThickness = BorderThickness,
                HorizontalOptions = HorizontalAlignment,
                StrokeShape = StrokeShape,

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

            Console.WriteLine($"IsLink: {IsLink}");
            Console.WriteLine($"Link: {Link}");

            if (IsLink)
            {
                var linkGesture = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
                linkGesture.Tapped += async (s, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(Link))
                    {
                        try
                        {
                            Console.WriteLine($"Link chamado: {Link}");
                            Uri uri = new(Link);
                            await Launcher.OpenAsync(uri);
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível abrir o link.", "OK");
                            Debug.WriteLine($"Failed to open link: {ex.Message}");
                        }
                    }
                };

                label.GestureRecognizers.Add(linkGesture);
            }


            container.GestureRecognizers.Add(actiongesture);
            ViewReference = container;
            container.BindingContext = this;

            return container;
        }
    }
}
