using InFeminine_Admin.Repositories;
using InFeminine_Admin.ViewModels.Article_ViewModels;

namespace InFeminine_Admin.Views.Articles;

public partial class Article_01 : ContentPage
{
	public Article_01()
	{
		InitializeComponent();
        Article_01_VM VM = new() { ContentLayout = DynamicContentArea };
        BindingContext = VM;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is Article_01_VM vm)
        {
            vm.BackgroundColor = GlobalVariables.BackgroundColor;
            vm.Title = GlobalVariables.PageTitles[1];
        }
    }
}