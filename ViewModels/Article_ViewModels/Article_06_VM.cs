using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using InFeminine_Admin.Repositories;
using InFeminine_Admin.Views.Custom;

namespace InFeminine_Admin.ViewModels.Article_ViewModels
{
    public partial class Article_06_VM : BasePageViewModel
    {
        [ObservableProperty] public Color backgroundColor = Colors.White;

        protected override async Task<Color> ShowColorPickerAsync()
        {
            string hexColor = BackgroundColor.ToRgbaHex(true);
            var colorPicker = new ColorPicker(hexColor);
            var color = await Application.Current.MainPage.ShowPopupAsync(colorPicker);

            if (color is string Hex && ContentLayout is not null)
            {
                GlobalVariables.BackgroundColor = Color.FromRgba(Hex);
                BackgroundColor = GlobalVariables.BackgroundColor;
                return Color.FromRgba(Hex) ?? Colors.White;
            }
            return Colors.White;
        }
    }
}