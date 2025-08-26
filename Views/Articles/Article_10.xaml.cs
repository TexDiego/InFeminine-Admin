using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_10 : ContentPage
{
	public Article_10()
	{
		InitializeComponent();
		Article_10_VM VM = new();
		VM.ContentLayout = DynamicContentArea;
		BindingContext = VM;
    }
}