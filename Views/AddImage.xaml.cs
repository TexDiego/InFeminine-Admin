using InFeminine_Admin.Interfaces;
using InFeminine_Admin.Models;
using InFeminine_Admin.ViewModels;

namespace InFeminine_Admin.Views;

public partial class AddImage : ContentPage
{
    private readonly AddImage_ViewModel vm;
    private TaskCompletionSource<IVisualBlock> _tcs;
    private ImageBlock _blockToEdit;

    public AddImage()
    {
        InitializeComponent();
        vm = new();
        BindingContext = vm;
    }

    public AddImage(ImageBlock block) : this()
    {
        _blockToEdit = block;
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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_blockToEdit is not null)
        {
            vm.SetBlock(_blockToEdit);
            _blockToEdit = null;
        }
    }

    protected override void OnDisappearing()
    {
        if (!_tcs.Task.IsCompleted)
            _tcs.SetResult(null);
    }
}