using Microsoft.Extensions.Logging;
using InFeminine_Admin.ViewModels;
using CommunityToolkit.Maui;
using InFeminine_Admin.Repositories;

namespace InFeminine_Admin
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Registering the ViewModels
            builder.Services.AddTransient<Home_ViewModel>();
            builder.Services.AddTransient<AddText_ViewModel>();
            builder.Services.AddTransient<AddArticle_ViewModel>();
            builder.Services.AddTransient<AddImage_ViewModel>();

            return builder.Build();
        }
    }
}
