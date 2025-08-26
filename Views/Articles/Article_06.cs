using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_06 : ContentPage
{
	public Article_06()
	{
		InitializeComponent();
		Article_06_VM VM = new();
		VM.ContentLayout = DynamicContentArea;
		BindingContext = VM;
    }
}