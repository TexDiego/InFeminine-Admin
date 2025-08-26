using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using InFeminine_Admin.Interfaces;
using InFeminine_Admin.Models;
using InFeminine_Admin.Repositories;
using InFeminine_Admin.Views;
using InFeminine_Admin.Views.Custom;

namespace InFeminine_Admin.ViewModels.Article_ViewModels
{
    public abstract partial class BasePageViewModel : ObservableObject
    {
        [ObservableProperty] private Layout? contentLayout;
        [ObservableProperty] private Color backGroundColor = GlobalVariables.BackgroundColor;
        [ObservableProperty] private string title = "Artigo";

        public Dictionary<Guid, IVisualBlock> _blocks = new();

        public Command AddContent => new(async () => await AddContentAsync());
        public Command ShowColorPicker => new(async () => await ShowColorPickerAsync());
        public Command EditContent => new(async (View) => await EditContentAsync(View as IVisualBlock));


        protected virtual async Task AddContentAsync()
        {
            var item = await Application.Current.MainPage.ShowPopupAsync(new ContentPicker());

            if (item != null)
            {
                switch (item)
                {
                    case "Carrossel":
                        var articlepage = new AddArticle();
                        var articleTask = articlepage.GetBlockAsync();
                        await Application.Current.MainPage.Navigation.PushAsync(articlepage);
                        var articleblock = await articleTask;
                        AddBlock(articleblock);
                        break;

                    case "Texto":
                        var textpage = new AddText();
                        var blockTask = textpage.GetBlockAsync();
                        await Application.Current.MainPage.Navigation.PushAsync(textpage);
                        var textblock = await blockTask;
                        AddBlock(textblock);
                        break;

                    case "Imagem":
                        var imagePage = new AddImage();
                        await Application.Current.MainPage.Navigation.PushAsync(imagePage);
                        var imageBlock = await imagePage.GetBlockAsync();
                        AddBlock(imageBlock);
                        break;
                }
            }
        }

        protected void AddBlock(IVisualBlock? block)
        {
            if (block == null || ContentLayout == null)
                return;

            block.ActionRequested = async (b) => await ActionSwapper(b);

            var guid = Guid.NewGuid();
            _blocks.Add(guid, block);
            ContentLayout.Children.Add(block.BuildView());
        }

        protected virtual async Task ActionSwapper(IVisualBlock? contentView)
        {
            var action = await Application.Current.MainPage.ShowPopupAsync(new ContentAction());

            if (action is null)
                return;

            if (action == "edit") await EditContentAsync(contentView);

            else if (action == "delete")
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert(
                    "Confirmar Exclusão",
                    "Você tem certeza que deseja excluir este conteúdo?",
                    "Sim", "Não");

                if (!confirm) return;

                    if (contentView is not null && ContentLayout is not null)
                {
                    var index = ContentLayout.Children.IndexOf(contentView.ViewReference);
                    if (index >= 0)
                    {
                        ContentLayout.Children.RemoveAt(index);
                        _blocks.Remove(contentView.Guid);
                    }
                }
            }
        }

        protected virtual async Task EditContentAsync(IVisualBlock? contentView)
        {
            if (contentView is null || ContentLayout is null)
                return;

            IVisualBlock? updatedBlock = null;

            switch (contentView)
            {
                case ImageBlock imageBlock:
                    var imagePage = new AddImage(imageBlock);
                    await Application.Current.MainPage.Navigation.PushAsync(imagePage);
                    updatedBlock = await imagePage.GetBlockAsync();
                    break;

                case TextBlock textBlock:
                    var textPage = new AddText(textBlock);
                    await Application.Current.MainPage.Navigation.PushAsync(textPage);
                    updatedBlock = await textPage.GetBlockAsync();
                    break;

                case ArticleBlock articleBlock:
                    var articlePage = new AddArticle(articleBlock);
                    await Application.Current.MainPage.Navigation.PushAsync(articlePage);
                    updatedBlock = await articlePage.GetBlockAsync();
                    break;
            }

            if (updatedBlock == null)
                return;

            updatedBlock.ActionRequested = async (b) => await ActionSwapper(b);
            _blocks[contentView.Guid] = updatedBlock;

            var index = ContentLayout.Children.IndexOf(contentView.ViewReference);
            if (index >= 0)
            {
                ContentLayout.Children.RemoveAt(index);
                ContentLayout.Children.Insert(index, updatedBlock.BuildView());
            }
        }

        protected virtual async Task<Color> ShowColorPickerAsync()
        {
            string hexColor = BackGroundColor.ToRgbaHex(true);
            var colorPicker = new ColorPicker(hexColor);
            var color = await Application.Current.MainPage.ShowPopupAsync(colorPicker);

            if (color is string hex && ContentLayout is not null)
            {
                return Color.FromRgba(hex) ?? Colors.White;
            }
            return Colors.White;
        }
    }
}