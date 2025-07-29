using InFeminine_Admin.Interfaces;
using InFeminine_Admin.ViewModels;

namespace InFeminine_Admin.Views;

public partial class Home_AddImage : ContentPage
{
    private readonly Home_AddImage_ViewModel vm;
    private TaskCompletionSource<IVisualBlock> _tcs;

    public Home_AddImage()
    {
        InitializeComponent();
        vm = new();
        BindingContext = vm;
    }

    public Task<IVisualBlock> GetBlockAsync()
    {
        _tcs = new();
        return _tcs.Task;
    }

    private async void OnSave(object sender, EventArgs e)
    {
        var bloco = vm.CreateBlock();
        _tcs.SetResult(bloco);
        await Navigation.PopAsync();
    }

    protected override void OnDisappearing()
    {
        if (!_tcs.Task.IsCompleted)
            _tcs.SetResult(null);
    }
}