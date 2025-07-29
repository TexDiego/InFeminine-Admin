using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using InFeminine_Admin.Models;
using InFeminine_Admin.Views.Custom;
using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;

namespace InFeminine_Admin.ViewModels
{
    public partial class Home_AddImage_ViewModel : ObservableObject
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

        [ObservableProperty] private double leftMargin = 5;
        [ObservableProperty] private double rightMargin = 5;
        [ObservableProperty] private double topMargin = 5;
        [ObservableProperty] private double bottomMargin = 5;
        [ObservableProperty] private double allMargin = 5;
        [ObservableProperty] private Thickness margin = new(5, 5, 5, 5);

        public ICommand PickImageCommand => new Command(async () => await PickImageAsync());
        public ICommand ToggleFlipHorizontalCommand => new Command(() => FlipHorizontally = !FlipHorizontally);
        public ICommand ToggleFlipVerticalCommand => new Command(() => FlipVertically = !FlipVertically);
        public ICommand FillCommand => new Command(() => Aspect = Aspect.Fill);
        public ICommand AspectFitCommand => new Command(() => Aspect = Aspect.AspectFit);
        public ICommand AspectFillCommand => new Command(() => Aspect = Aspect.AspectFill);
        public ICommand PickStrokeColor => new Command(async () => await PickStroke());
        public ICommand PickBackgroundColor => new Command(async () => await PickBackground());
        public ICommand LeftAlignment => new Command(() => HorizontalAlignment = LayoutOptions.Start);
        public ICommand CenterAlignment => new Command(() => HorizontalAlignment = LayoutOptions.Center);
        public ICommand RightAlignment => new Command(() => HorizontalAlignment = LayoutOptions.End);
        public ICommand FillAlignment => new Command(() => HorizontalAlignment = LayoutOptions.Fill);


        private async Task PickStroke()
        {
            var popup = new ColorPicker();
            var color = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (color is string hexColor)
            {
                StrokeColor = Color.FromArgb(hexColor);
            }
        }

        private async Task PickBackground()
        {
            var popup = new ColorPicker();
            var color = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (color is string hexColor)
            {
                BackGroundColor = Color.FromArgb(hexColor);
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
            int width = string.IsNullOrEmpty(value)? 0 : int.Parse(value);

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

        partial void OnImageOpacitySliderChanged(double value)
        {
            ImageOpacityText = value.ToString();
            ImageOpacity = value / 100;
        }

        partial void OnImageOpacityTextChanged(string value)
        {
            double opacity = string.IsNullOrEmpty(value)? 0 : double.Parse(value);

            if (opacity >= 10 && opacity <= 100)
            {
                ImageOpacity = opacity / 100;
                ImageOpacityText = opacity.ToString();
                ImageOpacitySlider = opacity;
            }            
        }

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

        public ImageBlock? CreateBlock()
        {
            return HasImage
                ? new ImageBlock
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
                    Margin = Margin
                }
                : null;
        }
    }
}