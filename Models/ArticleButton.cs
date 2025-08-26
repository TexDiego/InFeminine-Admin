using CommunityToolkit.Mvvm.ComponentModel;

namespace InFeminine_Admin.Models
{
    public partial class ArticleButton : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string text = string.Empty;

        [ObservableProperty]
        private ImageSource image;

        [ObservableProperty]
        private int borderWidth = 1;

        [ObservableProperty]
        private Color borderColor = Colors.Black;

        [ObservableProperty]
        private Color backgroundColor = Colors.Transparent;

        [ObservableProperty]
        private Color textColor = Colors.Black;

        [ObservableProperty]
        private int fontSize = 14;

        [ObservableProperty]
        private int borderRadius = 5;

        [ObservableProperty]
        private Thickness margin = new Thickness(5, 5, 5, 5);

        [ObservableProperty]
        private bool isEnabled = true;

        [ObservableProperty]
        private int height = 200;

        [ObservableProperty]
        private Command command;
    }
}

