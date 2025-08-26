using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_05 : ContentPage
{
	public Article_05()
	{
		InitializeComponent();
		Article_05_VM VM = new();
		VM.ContentLayout = DynamicContentArea;
		BindingContext = VM;
    }
}