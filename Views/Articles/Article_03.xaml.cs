using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_03 : ContentPage
{
	public Article_03()
	{
		InitializeComponent();
        Article_03_VM VM = new();
        VM.ContentLayout = DynamicContentArea;
        BindingContext = VM;
    }
}