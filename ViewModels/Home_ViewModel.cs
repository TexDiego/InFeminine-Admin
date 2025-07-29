using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using InFeminine_Admin.Views;
using InFeminine_Admin.Views.Custom;

namespace InFeminine_Admin.ViewModels
{
    public partial class Home_ViewModel : ObservableObject
    {
        [ObservableProperty] private Layout? contentLayout;
        [ObservableProperty] private Color backGroundColor = Colors.White;

        public Command AddContent => new(async () => await AddContentAsync());
        public Command ShowColorPicker => new(async () => await ShowColorPickerAsync());

        private async Task AddContentAsync()
        {
            var item = await Application.Current.MainPage.ShowPopupAsync(new Home_ContentPicker());

            if (item != null)
            {
                switch (item)
                {
                    case "Artigo":
                        await Application.Current.MainPage.Navigation.PushAsync(new Home_AddArticle());
                        break;

                    case "Texto":

                        var page = new Home_AddText();
                        var blocoTask = page.GetBlockAsync();
                        await Application.Current.MainPage.Navigation.PushAsync(page);
                        var bloco = await blocoTask;

                        if (bloco != null)
                        {
                            ContentLayout?.Children.Add(bloco.BuildView());
                        }

                        break;

                    case "Imagem":

                        var imagePage = new Home_AddImage();

                        await Application.Current.MainPage.Navigation.PushAsync(imagePage);

                        var imageBlock = await imagePage.GetBlockAsync();

                        if (imageBlock != null)
                            ContentLayout?.Children.Add(imageBlock.BuildView());

                        break;

                    default:
                        break;
                }
            }
        }

        private async Task ShowColorPickerAsync()
        {
            string hexColor = BackGroundColor.ToHex();
            var colorPicker = new ColorPicker(hexColor);
            var color = await Application.Current.MainPage.ShowPopupAsync(colorPicker);

            if (color is string hex && ContentLayout is not null)
            {
                BackGroundColor = Color.FromArgb(hex) ?? Colors.White;
            }
        }
    }
}
