using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_01 : ContentPage
{
	public Article_01()
	{
		InitializeComponent();
        Article_01_VM VM = new();
        VM.ContentLayout = DynamicContentArea;
        BindingContext = VM;
    }
}