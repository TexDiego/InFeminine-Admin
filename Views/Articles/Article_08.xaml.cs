using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_08 : ContentPage
{
	public Article_08()
	{
		InitializeComponent();
		Article_08_VM VM = new();
		VM.ContentLayout = DynamicContentArea;
		BindingContext = VM;
    }
}