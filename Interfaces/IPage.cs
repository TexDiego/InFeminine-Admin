namespace InFeminine_Admin.Interfaces
{
    public interface IPage
    {
        ContentPage BuildPage();
        Guid Guid { get; }
        Action<IVisualBlock>? ActionRequested { get; set; }
        string Title { get; set; }
        Color BackGroundColor { get; set; }
    }
}