using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using InFeminine_Admin.Models;
using InFeminine_Admin.Views.Custom;
using System.Windows.Input;

namespace InFeminine_Admin.ViewModels
{
    public partial class Home_AddText_ViewModel : ObservableObject
    {
        private readonly int _maxSize = 30;
        private readonly int _minSize = 8;

        [ObservableProperty] private double size = 12;
        [ObservableProperty] private string text = string.Empty;
        [ObservableProperty] private bool isBold = false;
        [ObservableProperty] private bool isItalic = false;
        [ObservableProperty] private FontAttributes attributes = FontAttributes.None;
        [ObservableProperty] private TextAlignment alignment = TextAlignment.Start;
        [ObservableProperty] private Color textColor = Colors.Black;

        [ObservableProperty] private double leftMargin = 5;
        [ObservableProperty] private double rightMargin = 5;
        [ObservableProperty] private double topMargin = 5;
        [ObservableProperty] private double bottomMargin = 5;
        [ObservableProperty] private double allMargin = 5;
        [ObservableProperty] private Thickness margin = new(5,5,5,5);

        public Home_AddText_ViewModel()
        {
            UpdateFontAttributes();
            UpdateMargin();
        }

        public ICommand IncreaseFontSize => new Command(() =>
        {
            if (this.Size < _maxSize)
                this.Size += 1;
        });

        public ICommand DecreaseFontSize => new Command(() =>
        {
            if (this.Size > _minSize)
                this.Size -= 1;
        });

        public ICommand ApplyBold => new Command(() =>
        {
            IsBold = !IsBold;
        });

        public ICommand ApplyItalic => new Command(() =>
        {
            IsItalic = !IsItalic;
        });

        public ICommand Left => new Command(() =>
        {
            Alignment = TextAlignment.Start;
        });

        public ICommand Center => new Command(() =>
        {
            Alignment = TextAlignment.Center;
        });

        public ICommand Right => new Command(() =>
        {
            Alignment = TextAlignment.End;
        });

        public ICommand Justify => new Command(() =>
        {
            Alignment = TextAlignment.Justify;
        });

        public ICommand ChangeTextColor => new Command(async () =>
        {
            var popup = new ColorPicker();
            var color = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (color is string hexColor)
            {
                TextColor = Color.FromArgb(hexColor);
            }
        });

        partial void OnIsBoldChanged(bool value)
        {
            UpdateFontAttributes();
        }

        partial void OnIsItalicChanged(bool value)
        {
            UpdateFontAttributes();
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

        private void UpdateFontAttributes()
        {
            FontAttributes attr = FontAttributes.None;

            if (IsBold)
                attr |= FontAttributes.Bold;

            if (IsItalic)
                attr |= FontAttributes.Italic;

            this.Attributes = attr;
        }

        public TextBlock CreateBlock()
        {
            return new TextBlock
            {
                Text = this.Text,
                Size = this.Size,
                Attributes = this.Attributes,
                Alignment = this.Alignment,
                Margin = this.Margin,
                TextColor = TextColor
            };
        }
    }
}