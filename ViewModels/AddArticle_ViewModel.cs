using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using InFeminine_Admin.Interfaces;
using InFeminine_Admin.Models;
using InFeminine_Admin.Repositories;
using InFeminine_Admin.Views.Custom;
using System.Collections.ObjectModel;

namespace InFeminine_Admin.ViewModels
{
    public partial class AddArticle_ViewModel : ObservableObject
    {
        [ObservableProperty] private ObservableCollection<ArticleButton> buttons = new();
        private Dictionary<int, bool> buttonsUsed;

        [ObservableProperty] private Color backGroundColor = Colors.Gray;
        [ObservableProperty] private string text = "Novo artigo";
        [ObservableProperty] private ImageSource? image = null;
        [ObservableProperty] private int borderWidth = 1;
        [ObservableProperty] private Color borderColor = Colors.Black;
        [ObservableProperty] private Color textColor = Colors.Black;
        [ObservableProperty] private int fontSize = 14;
        [ObservableProperty] private int borderRadius = 5;
        [ObservableProperty] private Thickness margin = new(5, 5, 5, 5);
        [ObservableProperty] private int leftMargin = 5;
        [ObservableProperty] private int topMargin = 5;
        [ObservableProperty] private int rightMargin = 5;
        [ObservableProperty] private int bottomMargin = 5;
        [ObservableProperty] private int allMargin = 5;
        [ObservableProperty] private int height = 200;
        [ObservableProperty] private bool isLoop = true;

        [ObservableProperty] private bool hasButtons = false;
        [ObservableProperty] private int currentIndex = 0;

        public Color PreviewBackgroundColor { get => GlobalVariables.BackgroundColor; }

        public Command RemoveButtonCommand { get; set; }
        public Command AddButtonCommand { get; set; }
        public Command ChangeBackGroundColor { get; set; }
        public Command ChangeTextColor { get; set; }
        public Command ChangeBorderColor { get; set; }
        public Command SaveCommand { get; set; }



        public AddArticle_ViewModel()
        {
            AddButtonCommand = new Command(() => AddButton());
            RemoveButtonCommand = new Command<ArticleButton>(async (button) => await RemoveButton(button));
            ChangeBackGroundColor = new Command(async () => BackGroundColor = await ShowColorPickerAsync(BackGroundColor));
            ChangeTextColor = new Command(async () => TextColor = await ShowColorPickerAsync(TextColor));
            ChangeBorderColor = new Command(async () => BorderColor = await ShowColorPickerAsync(BorderColor));

            buttonsUsed = new Dictionary<int, bool>(GlobalVariables.PagesUsed);
        }

        private void UpdateButtonsList()
        {
            HasButtons = Buttons.Count > 0;
        }

        private void AddButton()
        {
            var unusedPage = GetUnusedPage();

            if (unusedPage == null || unusedPage == "Pagina0")
            {
                return;
            }

            int text = buttonsUsed.FirstOrDefault(x => x.Value == false).Key;

            var newButton = new ArticleButton()
            {
                BackgroundColor = BackGroundColor,
                BorderColor = BorderColor,
                BorderRadius = BorderRadius,
                BorderWidth = BorderWidth,
                FontSize = FontSize,
                Image = Image,
                Margin = Margin,
                Name = unusedPage,
                Text = Text,
                TextColor = TextColor,
                Height = Height,
                Command = GetCommand(text),
                IsEnabled = false
            };

            buttonsUsed[Convert.ToInt32(unusedPage.Replace("Pagina", ""))] = true;
            Buttons.Add(newButton);
            UpdateButtonsList();
        }

        private async Task RemoveButton(ArticleButton button)
        {
            if (Buttons.Count < 1)
            {
                Application.Current.MainPage.DisplayAlert("Ops", "Sem artigos para excluir.", "OK");
                return;
            }

            var choice = await Application.Current.MainPage.DisplayAlert(
                "Confirmar Exclusão",
                $"Você tem certeza que deseja excluir o item?",
                "Sim", "Não");

            if (!choice) return;

            buttonsUsed[Convert.ToInt32(button.Name.Replace("Pagina", ""))] = false;
            Buttons.Remove(button);
            UpdateButtonsList();
        }

        private Command GetCommand(int article)
        {
            return article switch
            {
                1 => new Command(() => Application.Current.MainPage.Navigation.PushAsync(GlobalVariables.Pages[0])),
                2 => new Command(() => Application.Current.MainPage.Navigation.PushAsync(GlobalVariables.Pages[1])),
                3 => new Command(() => Application.Current.MainPage.Navigation.PushAsync(GlobalVariables.Pages[2])),
                4 => new Command(() => Application.Current.MainPage.Navigation.PushAsync(GlobalVariables.Pages[3])),
                5 => new Command(() => Application.Current.MainPage.Navigation.PushAsync(GlobalVariables.Pages[4])),
                6 => new Command(() => Application.Current.MainPage.Navigation.PushAsync(GlobalVariables.Pages[5])),
                7 => new Command(() => Application.Current.MainPage.Navigation.PushAsync(GlobalVariables.Pages[6])),
                8 => new Command(() => Application.Current.MainPage.Navigation.PushAsync(GlobalVariables.Pages[7])),
                9 => new Command(() => Application.Current.MainPage.Navigation.PushAsync(GlobalVariables.Pages[8])),
                10 => new Command(() => Application.Current.MainPage.Navigation.PushAsync(GlobalVariables.Pages[9])),
                _ => throw new ArgumentOutOfRangeException(nameof(article), "Invalid article number")
            };
        }

        private string? GetUnusedPage()
        {
            var page = buttonsUsed.FirstOrDefault(x => x.Value == false);

            if (page.Key == 0)
            {
                Application.Current.MainPage.DisplayAlert("Erro", "Todas as páginas estão em uso. Remova uma página para adicionar outra.", "OK");
                return null;
            };

            return page.Key switch
            {
                1 => "Pagina1",
                2 => "Pagina2",
                3 => "Pagina3",
                4 => "Pagina4",
                5 => "Pagina5",
                6 => "Pagina6",
                7 => "Pagina7",
                8 => "Pagina8",
                9 => "Pagina9",
                10 => "Pagina10"
            };
        }

        private async Task<Color> ShowColorPickerAsync(Color Hex)
        {
            string hexColor = Hex.ToRgbaHex(true);
            var colorPicker = new ColorPicker(hexColor);
            var color = await Application.Current.MainPage.ShowPopupAsync(colorPicker);

            if (color is string hex)
            {
                return Color.FromRgba(hex) ?? Colors.White;
            }
            return Colors.White;
        }






        public IVisualBlock CreateBlock()
        {
            var block = new ArticleBlock
            {
                HeightRequest = Height + 10,
                Loop = IsLoop,
                Pages = [],
            };

            if (Buttons == null || Buttons.Count == 0)
                return block;

            foreach (var vmBtn in Buttons.Where(b => b != null))
            {
                block.Pages.Add(new Article
                {
                    Name = vmBtn.Name,
                    Text = vmBtn.Text,
                    ImageSource = vmBtn.Image,
                    BorderWidth = vmBtn.BorderWidth,
                    BorderColor = vmBtn.BorderColor,
                    BackgroundColor = vmBtn.BackgroundColor,
                    TextColor = vmBtn.TextColor,
                    FontSize = vmBtn.FontSize,
                    CornerRadius = vmBtn.BorderRadius,
                    Margin = vmBtn.Margin,
                    IsEnabled = true,
                    HeightRequest = vmBtn.Height,
                    Command = vmBtn.Command
                });
                GlobalVariables.PageTitles[Convert.ToInt32(vmBtn.Name.Replace("Pagina", ""))] = vmBtn.Text;
            }

            return block;
        }

        public void SetBlock(IVisualBlock articleBlock)
        {
            if (articleBlock == null || articleBlock is not ArticleBlock)
                return;

            Buttons.Clear();

            ArticleBlock Block = (ArticleBlock)articleBlock;
            HasButtons = Block.Pages.Count > 0;

            foreach (var btn in Block.Pages)
            {
                Buttons.Add(new ArticleButton
                {
                    Name = btn.Name,
                    Text = btn.Text,
                    Image = btn.ImageSource,
                    BorderWidth = (int)btn.BorderWidth,
                    BorderColor = btn.BorderColor,
                    BackgroundColor = btn.BackgroundColor,
                    TextColor = btn.TextColor,
                    FontSize = (int)btn.FontSize,
                    BorderRadius = btn.CornerRadius,
                    Margin = btn.Margin,
                    IsEnabled = false,
                    Height = (int)btn.HeightRequest,
                    Command = GetCommand(Convert.ToInt32(btn.Name.Replace("Pagina", "")))
                });
            }

            Height = Block.HeightRequest - 10;
        }

        public void OnSave()
        {
            foreach (var pages in buttonsUsed)
            {
                if (pages.Value == true)
                {
                    GlobalVariables.PagesUsed[pages.Key] = true;
                    GlobalVariables.Pages[pages.Key - 1].Title = Buttons.FirstOrDefault(b => b.Name == $"Pagina{pages.Key}")?.Text ?? $"Pagina{pages.Key}";
                    continue;
                }
            }
        }



        public async Task ToggleLoopAsync(bool enable)
        {
            if (!enable && Buttons?.Count > 0)
            {
                CurrentIndex = Math.Min(CurrentIndex, Buttons.Count - 1);
                CurrentIndex = 0;

                await Task.Delay(80);
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsLoop = enable;
            });
        }


        // Margins properties
        partial void OnAllMarginChanged(int value)
        {
            LeftMargin = value;
            RightMargin = value;
            TopMargin = value;
            BottomMargin = value;
            Margin = new Thickness(LeftMargin, TopMargin, RightMargin, BottomMargin);
        }

        partial void OnLeftMarginChanged(int value)
        {
            UpdateMargin();
        }

        partial void OnRightMarginChanged(int value)
        {
            UpdateMargin();
        }

        partial void OnTopMarginChanged(int value)
        {
            UpdateMargin();
        }

        partial void OnBottomMarginChanged(int value)
        {
            UpdateMargin();
        }

        private void UpdateMargin()
        {
            Margin = new Thickness(LeftMargin, TopMargin, RightMargin, BottomMargin);
        }
    }
}