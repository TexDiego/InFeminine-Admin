using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_07 : ContentPage
{
	public Article_07()
	{
		InitializeComponent();
		Article_07_VM VM = new();
		VM.ContentLayout = DynamicContentArea;
		BindingContext = VM;
    }
}