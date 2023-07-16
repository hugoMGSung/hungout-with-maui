using TodoApp.DataServices;
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

            builder.Services.AddTransient(provider => new HttpClient
            {
                BaseAddress = new Uri($@"http://{(DeviceInfo.DeviceType == DeviceType.Virtual
                    ? "192.168.45.190" : "localhost")}:5224/"),
                Timeout = TimeSpan.FromSeconds(10)
            });

            // Microsoft.Extensions.Http 추가
            builder.Services.AddHttpClient<IRestDataService, RestDataService>();
            builder.Services.AddSingleton<MainPage>(); //Add the page for dependency injection 
            builder.Services.AddTransient<ManageToDoPage>();
            //#if DEBUG
            //		builder.Logging.AddDebug();
            //#endif

#if ANDROID && DEBUG
            Platforms.Android.DangerousAndroidMessageHandlerEmitter.Register();
            Platforms.Android.DangerousTrustProvider.Register();
#endif

            return builder.Build();
        }
    }
}