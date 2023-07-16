using TodoApp.Pages;

namespace TodoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("NanumGothic.ttf", "Nanum");
                    fonts.AddFont("NanumGothicExtraBold.ttf", "NanumBold");
                });

            // Microsoft.Extensions.Http 추가
            builder.Services.AddSingleton<MainPage>(); //Add the page for dependency injection 
            builder.Services.AddTransient<ManageToDoPage>();

            return builder.Build();
        }
    }
}