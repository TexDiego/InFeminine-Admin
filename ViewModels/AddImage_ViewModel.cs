using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using InFeminine_Admin.Models;
using InFeminine_Admin.Repositories;
using InFeminine_Admin.Views.Custom;
using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;

namespace InFeminine_Admin.ViewModels
{
    public partial class AddImage_ViewModel : ObservableObject
    {
        [ObservableProperty] private ImageSource? imageSource;
        [ObservableProperty] private bool hasImage;

        [ObservableProperty] private int imageWidth = 100;
        [ObservableProperty] private string imageWidthText = "100";
        [ObservableProperty] private double imageWidthProportion = 100;

        [ObservableProperty] private int imageHeight = 100;
        [ObservableProperty] private string imageHeightText = "100";
        [ObservableProperty] private double imageHeightProportion = 100;

        [ObservableProperty] private int imageRotation = 0;
        [ObservableProperty] private string imageRotationText = "0";

        [ObservableProperty] private double imageOpacity = 1;
        [ObservableProperty] private string imageOpacityText = "100";
        [ObservableProperty] private double imageOpacitySlider = 100;

        [ObservableProperty] private Aspect aspect = Aspect.Fill;

        [ObservableProperty] private LayoutOptions horizontalAlignment = LayoutOptions.Center;

        [ObservableProperty] private bool flipHorizontally;
        [ObservableProperty] private bool flipVertically;

        [ObservableProperty] private double strokeThickness = 0;
        [ObservableProperty] private string strokeThicknessText = "0";

        [ObservableProperty] private IShape cornerRadius = new RoundRectangle { CornerRadius = 0 };
        [ObservableProperty] private double maxCornerRadiusValue = 50;
        [ObservableProperty] private double cornerRadiusValue = 0;
        [ObservableProperty] private string cornerRadiusText = "0";

        [ObservableProperty] private Color strokeColor = Colors.Transparent;
        [ObservableProperty] private Color backGroundColor = Colors.Transparent;

        [ObservableProperty] private double leftMargin = 0;
        [ObservableProperty] private double rightMargin = 0;
        [ObservableProperty] private double topMargin = 0;
        [ObservableProperty] private double bottomMargin = 0;
        [ObservableProperty] private double allMargin = 0;
        [ObservableProperty] private Thickness margin = new(0, 0, 0, 0);

        [ObservableProperty] private double leftPadding = 0;
        [ObservableProperty] private double rightPadding = 0;
        [ObservableProperty] private double topPadding = 0;
        [ObservableProperty] private double bottomPadding = 0;
        [ObservableProperty] private double allPadding = 0;
        [ObservableProperty] private Thickness padding = new Thickness(0, 0, 0, 0);

        [ObservableProperty] private float shadowRadius = 0f;
        [ObservableProperty] private string shadowRadiusText = "0";

        [ObservableProperty] private Color shadowColor = Colors.Transparent;

        [ObservableProperty] private float shadowOpacity = 0f;
        [ObservableProperty] private string shadowOpacityText = "0";

        [ObservableProperty] private Point shadowOffset = new Point(0, 0);
        [ObservableProperty] private double shadowOffsetX = 0;
        [ObservableProperty] private string shadowOffsetXText = "0";
        [ObservableProperty] private double shadowOffsetY = 0;
        [ObservableProperty] private string shadowOffsetYText = "0";

        public Color PreviewBackgroundColor { get => GlobalVariables.BackgroundColor; }




        public ICommand PickImageCommand => new Command(async () => await PickImageAsync());
        public ICommand ToggleFlipHorizontalCommand => new Command(() => FlipHorizontally = !FlipHorizontally);
        public ICommand ToggleFlipVerticalCommand => new Command(() => FlipVertically = !FlipVertically);
        public ICommand FillCommand => new Command(() => Aspect = Aspect.Fill);
        public ICommand AspectFitCommand => new Command(() => Aspect = Aspect.AspectFit);
        public ICommand AspectFillCommand => new Command(() => Aspect = Aspect.AspectFill);
        public ICommand PickStrokeColor => new Command(async () => await PickStroke());
        public ICommand PickBackgroundColor => new Command(async () => await PickBackground());
        public ICommand PickShadowColor => new Command(async () => await PickShadowColorAsync());
        public ICommand LeftAlignment => new Command(() => HorizontalAlignment = LayoutOptions.Start);
        public ICommand CenterAlignment => new Command(() => HorizontalAlignment = LayoutOptions.Center);
        public ICommand RightAlignment => new Command(() => HorizontalAlignment = LayoutOptions.End);
        public ICommand FillAlignment => new Command(() => HorizontalAlignment = LayoutOptions.Fill);





        // Image functions
        private async Task PickStroke()
        {
            string stroke = StrokeColor.ToRgbaHex(true);
            var popup = new ColorPicker(stroke);
            var color = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (color is string hexColor)
            {
                StrokeColor = Color.FromRgba(hexColor);
            }
        }

        private async Task PickBackground()
        {
            string background = BackGroundColor.ToRgbaHex(true);
            var popup = new ColorPicker(background);
            var color = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (color is string hexColor)
            {
                BackGroundColor = Color.FromRgba(hexColor);
            }
        }

        private async Task PickShadowColorAsync()
        {
            string hex = ShadowColor.ToRgbaHex(true);
            var popup = new ColorPicker(hex);
            var color = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (color is string hexColor)
            {
                ShadowColor = Color.FromRgba(hexColor);
            }
        }

        private async Task PickImageAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Selecione uma imagem",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    using var stream = await result.OpenReadAsync();
                    using var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    ImageSource = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                    HasImage = true;
                }
            }
            catch
            {
                HasImage = false;
            }
        }


        // Margins properties
        partial void OnAllMarginChanged(double value)
        {
            LeftMargin = value;
            RightMargin = value;
            TopMargin = value;
            BottomMargin = value;
            Margin = new Thickness(LeftMargin, TopMargin, RightMargin, BottomMargin);
        }

        partial void OnLeftMarginChanged(double value)
        {
            UpdateMargin();
        }

        partial void OnRightMarginChanged(double value)
        {
            UpdateMargin();
        }

        partial void OnTopMarginChanged(double value)
        {
            UpdateMargin();
        }

        partial void OnBottomMarginChanged(double value)
        {
            UpdateMargin();
        }

        private void UpdateMargin()
        {
            Margin = new Thickness(LeftMargin, TopMargin, RightMargin, BottomMargin);
        }


        // Padding properties
        partial void OnAllPaddingChanged(double value)
        {
            LeftPadding = value;
            RightPadding = value;
            TopPadding = value;
            BottomPadding = value;
            Padding = new Thickness(LeftPadding, TopPadding, RightPadding, BottomPadding);
        }

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

        private void UpdatePadding()
        {
            Padding = new Thickness(LeftPadding, TopPadding, RightPadding, BottomPadding);
        }


        // Image proportions functions
        private void UpdateHeightProportion(int value)
        {
            double screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            value = Math.Clamp(value, 50, 400);
            double t = (value - 50) / 350.0;

            double minScale = 1.0 / 8.0;
            double maxScale = 1.0;

            double scaleFactor = minScale + t * (maxScale - minScale);

            ImageHeightProportion = screenWidth * scaleFactor;
            MaxCornerRadiusValue = Math.Max(ImageHeight, ImageWidth) / 2.0;
        }

        private void UpdateWidthProportion(int value)
        {
            double screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            value = Math.Clamp(value, 50, 400);
            double t = (value - 50) / 350.0;

            double minScale = 1.0 / 8.0;
            double maxScale = 1.0;

            double scaleFactor = minScale + t * (maxScale - minScale);

            ImageWidthProportion = screenWidth * scaleFactor;
            MaxCornerRadiusValue = Math.Max(ImageHeight, ImageWidth) / 2.0;
        }


        // Image resizing properties
        partial void OnImageWidthChanged(int value)
        {
            const int min = 50;
            const int max = 400;

            if (value < min) ImageWidth = min;
            else if (value > max) ImageWidth = max;

            ImageWidthText = value.ToString();
            UpdateWidthProportion(value);
        }

        partial void OnImageHeightChanged(int value)
        {
            const int min = 50;
            const int max = 400;

            if (value < min) ImageHeight = min;
            else if (value > max) ImageHeight = max;

            ImageHeightText = value.ToString();
            UpdateHeightProportion(value);
        }

        partial void OnImageWidthTextChanged(string value)
        {
            int width = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);

            if (width >= 50 && width <= 400)
            {
                ImageWidth = width;
            }
        }

        partial void OnImageHeightTextChanged(string value)
        {
            int height = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);

            if (height >= 50 && height <= 400)
            {
                ImageHeight = height;
            }
        }


        // Image opacity properties
        partial void OnImageOpacitySliderChanged(double value)
        {
            ImageOpacityText = value.ToString();
            ImageOpacity = value / 100;
        }

        partial void OnImageOpacityTextChanged(string value)
        {
            double opacity = string.IsNullOrEmpty(value) ? 0 : double.Parse(value);

            if (opacity >= 10 && opacity <= 100)
            {
                ImageOpacity = opacity / 100;
                ImageOpacityText = opacity.ToString();
                ImageOpacitySlider = opacity;
            }
        }


        // Image rotation properties
        partial void OnImageRotationChanged(int value)
        {
            ImageRotationText = value.ToString();
        }

        partial void OnImageRotationTextChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            int rotation = int.Parse(value);

            ImageRotation = rotation % 360;
            ImageRotationText = ImageRotation.ToString();
        }


        // Corner radius properties
        partial void OnCornerRadiusValueChanged(double value)
        {
            CornerRadius = new RoundRectangle { CornerRadius = value };
            CornerRadiusText = value.ToString();
        }

        partial void OnCornerRadiusTextChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            double radius = double.Parse(value);

            if (radius >= 0 && radius <= 200)
                CornerRadiusValue = radius;
        }


        // Stroke thickness properties
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


        // Image constructor
        public ImageBlock? CreateBlock()
        {
            return HasImage ?
                new ImageBlock
                {
                    ImageSource = ImageSource,
                    Aspect = Aspect,
                    Width = ImageWidthProportion,
                    Height = ImageHeightProportion,
                    Rotation = ImageRotation,
                    Opacity = ImageOpacity,
                    ScaleX = FlipHorizontally ? -1 : 1,
                    ScaleY = FlipVertically ? -1 : 1,
                    Thickness = StrokeThickness,
                    CornerRadius = cornerRadius,
                    StrokeColor = StrokeColor,
                    BackGroundColor = BackGroundColor,
                    HorizontalOptions = HorizontalAlignment,
                    Margin = Margin,
                    Padding = new Thickness(LeftPadding, TopPadding, RightPadding, BottomPadding),
                    ShadowRadius = ShadowRadius,
                    ShadowColor = ShadowColor,
                    ShadowOpacity = ShadowOpacity,
                    ShadowOffset = ShadowOffset
                }
                : null;
        }

        public void SetBlock(ImageBlock imageBlock)
        {
            if (imageBlock is null) return;

            // BÁSICO
            HasImage = true;
            ImageSource = imageBlock.ImageSource;
            Aspect = imageBlock.Aspect;
            HorizontalAlignment = imageBlock.HorizontalOptions;

            // TAMANHO
            ImageWidthProportion = imageBlock.Width;
            ImageWidth = (int)imageBlock.Width;
            ImageWidthText = ImageWidth.ToString();

            ImageHeightProportion = imageBlock.Height;
            ImageHeight = (int)imageBlock.Height;
            ImageHeightText = ImageHeight.ToString();

            // ROTAÇÃO
            ImageRotation = imageBlock.Rotation;
            ImageRotationText = imageBlock.Rotation.ToString();

            // OPACIDADE
            ImageOpacity = imageBlock.Opacity;
            ImageOpacitySlider = imageBlock.Opacity * 100;
            ImageOpacityText = ((int)(imageBlock.Opacity * 100)).ToString();

            // FLIP
            FlipHorizontally = imageBlock.ScaleX < 0;
            FlipVertically = imageBlock.ScaleY < 0;

            // BORDA
            StrokeThickness = imageBlock.Thickness;
            StrokeThicknessText = StrokeThickness.ToString("0");

            StrokeColor = imageBlock.StrokeColor;
            BackGroundColor = imageBlock.BackGroundColor;

            CornerRadius = imageBlock.CornerRadius;
            // Tenta extrair o valor numérico do CornerRadius (se aplicável)
            if (imageBlock.CornerRadius is RoundRectangle roundRect)
            {
                CornerRadiusValue = roundRect.CornerRadius.TopLeft;
                CornerRadiusText = CornerRadiusValue.ToString("0");
            }

            // MARGIN            
            LeftMargin = imageBlock.Margin.Left;
            RightMargin = imageBlock.Margin.Right;
            TopMargin = imageBlock.Margin.Top;
            BottomMargin = imageBlock.Margin.Bottom;

            // PADDING            
            LeftPadding = imageBlock.Padding.Left;
            RightPadding = imageBlock.Padding.Right;
            TopPadding = imageBlock.Padding.Top;
            BottomPadding = imageBlock.Padding.Bottom;

            // SOMBRA
            ShadowRadius = imageBlock.ShadowRadius;
            ShadowRadiusText = ShadowRadius.ToString("0");

            ShadowColor = imageBlock.ShadowColor;

            ShadowOpacity = imageBlock.ShadowOpacity;
            ShadowOpacityText = (ShadowOpacity * 100).ToString("0");

            ShadowOffset = imageBlock.ShadowOffset;
            ShadowOffsetX = imageBlock.ShadowOffset.X;
            ShadowOffsetXText = ShadowOffsetX.ToString("0");
            ShadowOffsetY = imageBlock.ShadowOffset.Y;
            ShadowOffsetYText = ShadowOffsetY.ToString("0");
        }
    }
}