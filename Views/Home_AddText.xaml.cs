using InFeminine_Admin.Interfaces;
using InFeminine_Admin.ViewModels;
using System.Diagnostics;

namespace InFeminine_Admin.Views;

public partial class Home_AddText : ContentPage
{
	private TaskCompletionSource<IVisualBlock> _tcs;
    private Home_AddText_ViewModel vm;

    public Home_AddText()
	{
		InitializeComponent();
        vm = new();
		BindingContext = vm;
    }

    public Task<IVisualBlock> GetBlockAsync()
    {
        _tcs = new TaskCompletionSource<IVisualBlock>();
        return _tcs.Task;
    }

    private async void OnSave(object sender, EventArgs e)
    {
        var bloco = vm.CreateBlock();
        _tcs.SetResult(bloco);
        Debug.WriteLine($"Block created: {bloco}");
        await Navigation.PopAsync();
    }

    protected override void OnDisappearing()
    {
        if (!_tcs.Task.IsCompleted)
            _tcs.SetResult(null);
    }
}