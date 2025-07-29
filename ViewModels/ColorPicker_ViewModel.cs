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
        [ObservableProperty] private string hexColor = "#FFFFFF";
        [ObservableProperty] private Color previewColor = Colors.White;

        [ObservableProperty] private string redText = "255";
        [ObservableProperty] private string greenText = "255";
        [ObservableProperty] private string blueText = "255";

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
            UpdateFromRgb();
        }

        partial void OnGreenChanged(int value)
        {
            GreenText = value.ToString();
            UpdateFromRgb();
        }

        partial void OnBlueChanged(int value)
        {
            BlueText = value.ToString();
            UpdateFromRgb();
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

        partial void OnHexColorChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 7)
            {
                ValidColor = Colors.Red; // Adicionei uma propriedade para indicar a cor de validação com binding em uma border em volta da entry do HexCode
                return;
            }

            string hex = value.Trim().Replace("#", "");

            if (Regex.IsMatch(hex, @"^[0-9a-fA-F]{6}$"))
            {
                if (int.TryParse(hex.Substring(0, 2), NumberStyles.HexNumber, null, out int r) &&
                    int.TryParse(hex.Substring(2, 2), NumberStyles.HexNumber, null, out int g) &&
                    int.TryParse(hex.Substring(4, 2), NumberStyles.HexNumber, null, out int b))
                {
                    Red = r;
                    Green = g;
                    Blue = b;
                    PreviewColor = Color.FromRgb(r, g, b);
                    ValidColor = Colors.Green;
                }
            }
            else
            {
                ValidColor = Colors.Red;
            }
        }

        private void UpdateFromRgb()
        {
            HexColor = $"#{Red:X2}{Green:X2}{Blue:X2}";
            PreviewColor = Color.FromRgb(Red, Green, Blue);
        }
    }
}
