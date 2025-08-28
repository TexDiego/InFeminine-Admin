using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using InFeminine_Admin.Interfaces;
using InFeminine_Admin.Models;
using InFeminine_Admin.Repositories;
using InFeminine_Admin.Views.Custom;
using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;

namespace InFeminine_Admin.ViewModels
{
    public partial class AddText_ViewModel : ObservableObject
    {
        private readonly int _maxSize = 30;
        private readonly int _minSize = 8;

        // Text properties
        [ObservableProperty] private double size = 12;
        [ObservableProperty] private string text = string.Empty;
        [ObservableProperty] private bool isBold = false;
        [ObservableProperty] private bool isItalic = false;
        [ObservableProperty] private bool isUnderline = false;
        [ObservableProperty] private bool isStrikethrough = false;
        [ObservableProperty] private FontAttributes attributes = FontAttributes.None;
        [ObservableProperty] private TextDecorations decorations = TextDecorations.None;
        [ObservableProperty] private TextAlignment textAlignment = TextAlignment.Start;
        [ObservableProperty] private Color textColor = Colors.Black;
        [ObservableProperty] private bool hasText = false;

        // Border properties
        [ObservableProperty] private double leftMargin = 5;
        [ObservableProperty] private double rightMargin = 5;
        [ObservableProperty] private double topMargin = 5;
        [ObservableProperty] private double bottomMargin = 5;
        [ObservableProperty] private double allMargin = 5;
        [ObservableProperty] private Thickness margin = new(5, 5, 5, 5);

        [ObservableProperty] private LayoutOptions horizontalAlignment = LayoutOptions.Center;

        [ObservableProperty] private Color backGroundColor = Colors.Transparent;

        [ObservableProperty] private Color borderColor = Colors.Transparent;

        [ObservableProperty] private double strokeThickness = 0;
        [ObservableProperty] private string strokeThicknessText = "0";

        [ObservableProperty] private IShape strokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) };
        [ObservableProperty] private double strokeShapeValue = 0;
        [ObservableProperty] private string strokeShapeText = "0";

        [ObservableProperty] private double leftPadding = 0;
        [ObservableProperty] private double rightPadding = 0;
        [ObservableProperty] private double topPadding = 0;
        [ObservableProperty] private double bottomPadding = 0;
        [ObservableProperty] private double allPadding = 0;
        [ObservableProperty] private Thickness padding = new(0, 0, 0, 0);

        // Shadow properties
        [ObservableProperty] private float shadowRadius = 0f;
        [ObservableProperty] private string shadowRadiusText = "0";

        [ObservableProperty] private Color shadowColor = Colors.Transparent;

        [ObservableProperty] private float shadowOpacity = 0f;
        [ObservableProperty] private string shadowOpacityText = "0";

        [ObservableProperty] private Point shadowOffset = new(0, 0);
        [ObservableProperty] private double shadowOffsetX = 0;
        [ObservableProperty] private string shadowOffsetXText = "0";
        [ObservableProperty] private double shadowOffsetY = 0;
        [ObservableProperty] private string shadowOffsetYText = "0";

        // Link properties
        [ObservableProperty] private string link = string.Empty;
        [ObservableProperty] private bool isLink = false;

        // Preview background color
        public Color PreviewBackgroundColor { get => GlobalVariables.BackgroundColor; }

        public AddText_ViewModel()
        {
            UpdateFontAttributes();
            UpdateMargin();
            UpdatePadding();
            UpdateShadowOffset();
        }



        public ICommand IncreaseFontSize => new Command(() =>
        {
            if (this.Size < _maxSize)
                this.Size++;
        });

        public ICommand DecreaseFontSize => new Command(() =>
        {
            if (this.Size > _minSize)
                this.Size--;
        });

        public ICommand ApplyBold => new Command(() =>
        {
            IsBold = !IsBold;
        });

        public ICommand ApplyItalic => new Command(() =>
        {
            IsItalic = !IsItalic;
        });

        public ICommand ApplyUnderline => new Command(() =>
        {
            IsUnderline = !IsUnderline;
        });

        public ICommand ApplyStrikethrough => new Command(() =>
        {
            IsStrikethrough = !IsStrikethrough;
        });

        public ICommand Left => new Command(() =>
        {
            TextAlignment = TextAlignment.Start;
        });

        public ICommand Center => new Command(() =>
        {
            TextAlignment = TextAlignment.Center;
        });

        public ICommand Right => new Command(() =>
        {
            TextAlignment = TextAlignment.End;
        });

        public ICommand Justify => new Command(() =>
        {
            TextAlignment = TextAlignment.Justify;
        });

        public ICommand ChangeTextColor => new Command(async () => await PickTextColor());

        public ICommand LeftAlign => new Command(() =>
        {
            HorizontalAlignment = LayoutOptions.Start;
        });

        public ICommand CenterAlign => new Command(() =>
        {
            HorizontalAlignment = LayoutOptions.Center;
        });

        public ICommand RightAlign => new Command(() =>
        {
            HorizontalAlignment = LayoutOptions.End;
        });

        public ICommand PickStrokeColorCommand => new Command(async () => await PickStrokeColor());

        public ICommand PickBackgroundColorCommand => new Command(async () => await PickBackground());

        public ICommand PickShadowColorCommand => new Command(async () => await PickShadowColor());




        // Text propeties
        partial void OnTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                HasText = false;
            }
            else
            {
                HasText = true;
            }
        }

        partial void OnIsBoldChanged(bool value)
        {
            UpdateFontAttributes();
        }

        partial void OnIsItalicChanged(bool value)
        {
            UpdateFontAttributes();
        }

        partial void OnIsUnderlineChanged(bool value)
        {
           UpdateTextDecorations();
        }

        partial void OnIsStrikethroughChanged(bool value)
        {
            UpdateTextDecorations();
        }

        private void UpdateFontAttributes()
        {
            FontAttributes attr = FontAttributes.None;

            if (IsBold)
                attr |= FontAttributes.Bold;

            if (IsItalic)
                attr |= FontAttributes.Italic;

            this.Attributes = attr;
        }

        private void UpdateTextDecorations()
        {
            TextDecorations decor = TextDecorations.None;

            if (IsUnderline)
                decor |= TextDecorations.Underline;

            if (IsStrikethrough)
                decor |= TextDecorations.Strikethrough;

            Decorations = decor;
        }


        // Margin properties
        partial void OnAllMarginChanged(double value)
        {
            if (value > -100 && value < 100)
            {
                LeftMargin = value;
                RightMargin = value;
                TopMargin = value;
                BottomMargin = value;
                UpdateMargin();
            }
        }

        partial void OnLeftMarginChanged(double value)
        {
            if (value > -100 && value < 100)
                UpdateMargin();
        }

        partial void OnRightMarginChanged(double value)
        {
            if (value > -100 && value < 100)
                UpdateMargin();
        }

        partial void OnTopMarginChanged(double value)
        {
            if (value > -100 && value < 100)
                UpdateMargin();
        }

        partial void OnBottomMarginChanged(double value)
        {
            if (value > -100 && value < 100)
                UpdateMargin();
        }

        private void UpdateMargin()
        {
            Margin = new Thickness(LeftMargin, TopMargin, RightMargin, BottomMargin);
        }


        // Padding properties
        partial void OnLeftPaddingChanged(double value)
        {
            UpdatePadding();
        }

        partial void OnRightPaddingChanged(double value)
        {
            UpdatePadding();
        }

        partial void OnTopPaddingChanged(double value)
        {
            UpdatePadding();
        }

        partial void OnBottomPaddingChanged(double value)
        {
            UpdatePadding();
        }

        partial void OnAllPaddingChanged(double value)
        {
            LeftPadding = value;
            RightPadding = value;
            TopPadding = value;
            BottomPadding = value;
            Padding = new Thickness(LeftPadding, TopPadding, RightPadding, BottomPadding);
        }

        private void UpdatePadding()
        {
            Padding = new Thickness(LeftPadding, TopPadding, RightPadding, BottomPadding);
        }


        // StrokeThickness properties
        partial void OnStrokeThicknessChanged(double value)
        {
            StrokeThicknessText = value.ToString();
        }

        partial void OnStrokeThicknessTextChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            double thickness = double.Parse(value);

            StrokeThickness = Math.Max(0, thickness);
            StrokeThicknessText = StrokeThickness.ToString();
        }


        // StrokeShape properties
        partial void OnStrokeShapeValueChanged(double value)
        {
            StrokeShapeText = value.ToString();
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(value) };
        }

        partial void OnStrokeShapeTextChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            double radius = double.Parse(value);

            if (radius >= 0 && radius <= 100)
                StrokeShapeValue = radius;
        }


        // Color functions
        private async Task PickStrokeColor()
        {
            string strokeColor = BorderColor.ToRgbaHex(true);
            var popup = new ColorPicker(strokeColor);
            var color = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (color is string hexColor)
            {
                BorderColor = Color.FromRgba(hexColor);
            }
        }

        private async Task PickBackground()
        {
            string backgroundColor = BackGroundColor.ToRgbaHex(true);
            var popup = new ColorPicker(backgroundColor);
            var color = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (color is string hexColor)
            {
                BackGroundColor = Color.FromRgba(hexColor);
            }
        }

        private async Task PickShadowColor()
        {
            string shadowcolor = ShadowColor.ToRgbaHex(true);
            var popup = new ColorPicker(shadowcolor);
            var color = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (color is string hexColor)
            {
                ShadowColor = Color.FromRgba(hexColor);
            }
        }

        private async Task PickTextColor()
        {
            string textColor = TextColor.ToRgbaHex(true);
            var popup = new ColorPicker(textColor);
            var color = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (color is string hexColor)
            {
                TextColor = Color.FromRgba(hexColor);
            }
        }


        // Shadow radius properties
        partial void OnShadowRadiusChanged(float value)
        {
            ShadowRadiusText = value.ToString();
        }

        partial void OnShadowRadiusTextChanged(string value)
        {
            float radius = string.IsNullOrEmpty(value) ? 0 : float.Parse(value);

            if (radius >= 0 && radius <= 100)
            {
                ShadowRadius = radius;
            }
        }


        // Shadow opacity properties
        partial void OnShadowOpacityChanged(float value)
        {
            ShadowOpacityText = value.ToString();
        }

        partial void OnShadowOpacityTextChanged(string value)
        {
            float opacity = string.IsNullOrEmpty(value) ? 0 : float.Parse(value);
            if (opacity >= 0 && opacity <= 1)
            {
                ShadowOpacity = opacity;
            }
        }


        // Shadow offset properties
        partial void OnShadowOffsetXChanged(double value)
        {
            ShadowOffsetXText = value.ToString();
            UpdateShadowOffset();
        }

        partial void OnShadowOffsetYChanged(double value)
        {
            ShadowOffsetYText = value.ToString();
            UpdateShadowOffset();
        }

        partial void OnShadowOffsetXTextChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            double thickness = double.Parse(value);

            if (thickness >= -100 && thickness <= 100)
            {
                ShadowOffsetX = thickness;
                UpdateShadowOffset();
            }
        }

        partial void OnShadowOffsetYTextChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            double thickness = double.Parse(value);

            if (thickness >= -100 && thickness <= 100)
            {
                ShadowOffsetY = thickness;
                UpdateShadowOffset();
            }
        }

        private void UpdateShadowOffset()
        {
            ShadowOffset = new Point(ShadowOffsetX, ShadowOffsetY);
        }



        // Create a TextBlock instance with the current properties
        public TextBlock CreateBlock()
        {
            return new TextBlock
            {
                Text = this.Text,
                Size = this.Size,
                Attributes = this.Attributes,
                Decorations = this.Decorations,
                Alignment = this.TextAlignment,
                Margin = this.Margin,
                TextColor = this.TextColor,
                BackGroundColor = this.BackGroundColor,
                HorizontalAlignment = this.HorizontalAlignment,
                BorderColor = this.BorderColor,
                BorderThickness = this.StrokeThickness,
                StrokeShape = this.StrokeShape,
                ShadowRadius = this.ShadowRadius,
                ShadowColor = this.ShadowColor,
                ShadowOpacity = this.ShadowOpacity,
                ShadowOffset = this.ShadowOffset,
                Padding = new Thickness(LeftPadding, TopPadding, RightPadding, BottomPadding),
                Link = this.Link,
                IsLink = this.IsLink
            };
        }

        public void SetBlock(IVisualBlock content)
        {
            if (content is not null && content is TextBlock block)
            {
                Text = block.Text;
                Size = block.Size;
                Attributes = block.Attributes;
                Decorations = block.Decorations;
                TextAlignment = block.Alignment;
                Margin = block.Margin;
                TextColor = block.TextColor;
                BackGroundColor = block.BackGroundColor;
                HorizontalAlignment = block.HorizontalAlignment;
                BorderColor = block.BorderColor;
                StrokeThickness = block.BorderThickness;
                StrokeShape = block.StrokeShape;
                ShadowRadius = block.ShadowRadius;
                ShadowColor = block.ShadowColor;
                ShadowOpacity = block.ShadowOpacity;
                ShadowOffset = block.ShadowOffset;
                LeftPadding = block.Padding.Left;
                RightPadding = block.Padding.Right;
                TopPadding = block.Padding.Top;
                BottomPadding = block.Padding.Bottom;
                Link = block.Link;
                IsLink = block.IsLink;
                UpdateFontAttributes();
                UpdateMargin();
                UpdatePadding();
            }
        }
    }
}