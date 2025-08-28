using InFeminine_Admin.Repositories;
using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_07 : ContentPage
{
	public Article_07()
	{
		InitializeComponent();
        Article_07_VM VM = new() { ContentLayout = DynamicContentArea };
        BindingContext = VM;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is Article_07_VM vm)
        {
            vm.BackgroundColor = GlobalVariables.BackgroundColor;
            vm.Title = GlobalVariables.PageTitles[7];
        }
    }
}