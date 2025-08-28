using CommunityToolkit.Maui;
using InFeminine_Admin.ViewModels;
using InFeminine_Admin.ViewModels.Article_ViewModels;
using Microsoft.Extensions.Logging;

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

            builder.Services.AddTransient<Article_01_VM>();
            builder.Services.AddTransient<Article_02_VM>();
            builder.Services.AddTransient<Article_03_VM>();
            builder.Services.AddTransient<Article_04_VM>();
            builder.Services.AddTransient<Article_05_VM>();
            builder.Services.AddTransient<Article_06_VM>();
            builder.Services.AddTransient<Article_07_VM>();
            builder.Services.AddTransient<Article_08_VM>();
            builder.Services.AddTransient<Article_09_VM>();
            builder.Services.AddTransient<Article_10_VM>();

            return builder.Build();
        }
    }
}