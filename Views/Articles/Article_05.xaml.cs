using InFeminine_Admin.Repositories;
using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_05 : ContentPage
{
	public Article_05()
	{
		InitializeComponent();
        Article_05_VM VM = new() { ContentLayout = DynamicContentArea };
        BindingContext = VM;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is Article_05_VM vm)
        {
            vm.BackgroundColor = GlobalVariables.BackgroundColor;
            vm.Title = GlobalVariables.PageTitles[5];
        }
    }
}