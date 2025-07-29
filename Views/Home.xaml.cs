using InFeminine_Admin.ViewModels;

namespace InFeminine_Admin.Views;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
		Home_ViewModel home_ViewModel = new();
        home_ViewModel.ContentLayout = DynamicContentArea;
        BindingContext = home_ViewModel;
    }
}