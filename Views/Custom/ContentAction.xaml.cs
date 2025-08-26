using CommunityToolkit.Maui.Views;

namespace InFeminine_Admin.Views.Custom;

public partial class ContentAction : Popup
{
	public ContentAction()
	{
		InitializeComponent();
	}

    private void Edit_Clicked(object sender, EventArgs e)
    {
        this.Close("edit");
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        this.Close("delete");
    }
}