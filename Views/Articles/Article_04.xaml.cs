using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_04 : ContentPage
{
	public Article_04()
	{
		InitializeComponent();
        Article_04_VM VM = new();
        VM.ContentLayout = DynamicContentArea;
        BindingContext = VM;
    }
}