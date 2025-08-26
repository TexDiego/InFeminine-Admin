using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace InFeminine_Admin.ViewModels
{
    public partial class ColorPicker_ViewModel : ObservableObject
    {
        [ObservableProperty] private int red = 255;
        [ObservableProperty] private int green = 255;
        [ObservableProperty] private int blue = 255;
        [ObservableProperty] private int alpha = 255;

        [ObservableProperty] private string hexColor = "#FFFFFFFF";
        [ObservableProperty] private Color previewColor = Colors.White;

        [ObservableProperty] private string redText = "255";
        [ObservableProperty] private string greenText = "255";
        [ObservableProperty] private string blueText = "255";
        [ObservableProperty] private string alphaText = "255";

        [ObservableProperty] private Color validColor = Colors.Green;

        public double BorderWidth { get; }


        public ColorPicker_ViewModel()
        {
            double screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            BorderWidth = screenWidth - 50;
        }


        partial void OnRedChanged(int value)
        {
            RedText = value.ToString();
            UpdateFromRgba();
        }

        partial void OnGreenChanged(int value)
        {
            GreenText = value.ToString();
            UpdateFromRgba();
        }

        partial void OnBlueChanged(int value)
        {
            BlueText = value.ToString();
            UpdateFromRgba();
        }

        partial void OnAlphaChanged(int value)
        {
            AlphaText = value.ToString();
            UpdateFromRgba();
        }

        partial void OnRedTextChanged(string value)
        {
            if (int.TryParse(value, out int parsed) && parsed >= 0 && parsed <= 255)
                Red = parsed;
        }

        partial void OnGreenTextChanged(string value)
        {
            if (int.TryParse(value, out int parsed) && parsed >= 0 && parsed <= 255)
                Green = parsed;
        }

        partial void OnBlueTextChanged(string value)
        {
            if (int.TryParse(value, out int parsed) && parsed >= 0 && parsed <= 255)
                Blue = parsed;
        }

        partial void OnAlphaTextChanged(string value)
        {
            if (int.TryParse(value, out int parsed) && parsed >= 0 && parsed <= 255)
                Alpha = parsed;
        }

        partial void OnHexColorChanged(string value)
        {
            ValidColor = Colors.Red;

            if (string.IsNullOrWhiteSpace(value))
                return;

            string hex = value.Trim().Replace("#", "");

            if (hex.Length == 8 && Regex.IsMatch(hex, "^[0-9a-fA-F]{8}$"))
            {
                try
                {
                    byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                    byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                    byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
                    byte a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);

                    Red = r;
                    Green = g;
                    Blue = b;
                    Alpha = a;

                    PreviewColor = Color.FromRgba(r, g, b, a);
                    ValidColor = Colors.Green;
                }
                catch
                {
                    // Parsing falhou, mantém cor inválida
                }
            }
        }


        private void UpdateFromRgba()
        {
            HexColor = $"#{Red:X2}{Green:X2}{Blue:X2}{Alpha:X2}";
            PreviewColor = Color.FromRgba(Red, Green, Blue, Alpha);
        }
    }
}
