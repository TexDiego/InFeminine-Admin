using CommunityToolkit.Maui.Views;
using InFeminine_Admin.ViewModels;

namespace InFeminine_Admin.Views.Custom;

public partial class ColorPicker : Popup
{
	private ColorPicker_ViewModel vm;

	public ColorPicker()
	{
		vm = new();
        InitializeComponent();
		BindingContext = vm;
    }

    public ColorPicker(string hexColor) : this()
    {
        vm.HexColor = hexColor;
    }

    private void Save_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is ColorPicker_ViewModel vm)
        {
            Close(vm.HexColor);
        }
    }
}