using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_02 : ContentPage
{
	public Article_02()
	{
		InitializeComponent();
        Article_02_VM VM = new();
        VM.ContentLayout = DynamicContentArea;
        BindingContext = VM;
    }
}