using System.Windows.Input;

namespace InFeminine_Admin.Models
{
    public class Article
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public ImageSource ImageSource { get; set; }
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public Color BorderColor { get; set; }
        public int FontSize { get; set; }
        public int CornerRadius { get; set; }
        public Thickness Margin { get; set; }
        public int BorderRadius { get; set; }
        public int BorderWidth { get; set; }
        public int HeightRequest { get; set; }
        public ICommand Command { get; set; }
        public bool IsEnabled { get; set; }
    }
}