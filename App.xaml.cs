using InFeminine_Admin.ViewModels;
using InFeminine_Admin.Views;

namespace InFeminine_Admin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new Home()));
        }
    }
}