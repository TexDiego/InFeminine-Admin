using InFeminine_Admin.Interfaces;
using InFeminine_Admin.Models;
using InFeminine_Admin.ViewModels;
using System.Diagnostics;

namespace InFeminine_Admin.Views;

public partial class AddArticle : ContentPage
{
    private readonly AddArticle_ViewModel vm;
    private TaskCompletionSource<IVisualBlock> _tcs = new();
    private IVisualBlock _blockToEdit;

    public AddArticle()
	{
		InitializeComponent();
        vm = new();
        BindingContext = vm;        
    }

    public AddArticle(IVisualBlock block) : this()
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
        vm.OnSave();
        _tcs.SetResult(bloco);
        await Navigation.PopAsync();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_blockToEdit is not null)
        {
            vm.SetBlock((ArticleBlock)_blockToEdit);
            _blockToEdit = null;
        }
    }

    protected override void OnDisappearing()
    {
        if (_tcs != null && !_tcs.Task.IsCompleted)
            _tcs.SetResult(null);
    }

    private async void CarouselLooping_CB_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            Carousel.ItemsSource = null;
            await vm.ToggleLoopAsync(e.Value);
            Carousel.ItemsSource = vm.Buttons;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro ToggleLoop: {ex}");
        }
    }

}