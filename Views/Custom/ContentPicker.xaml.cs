using CommunityToolkit.Maui.Views;

namespace InFeminine_Admin.Views;

public partial class ContentPicker : Popup
{
	private string _selectedItem;

    public ContentPicker()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        this.Close(_selectedItem);
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        _selectedItem = (sender as RadioButton)?.Content.ToString();
    }
}