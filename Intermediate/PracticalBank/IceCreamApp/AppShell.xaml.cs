using IceCreamApp.Pages;

namespace IceCreamApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(SigninPage), typeof(SigninPage));
            //Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
            RegisterRoutes(); 
        }

        private readonly static Type[] _routablePageTypes = 
            [
                typeof(SigninPage),
                typeof(SignupPage),
                typeof(MyOrdersPage),
                typeof(OrderDetailsPage),
                typeof(DetailsPage),
            ];

        private void RegisterRoutes()
        {
            foreach (var pageType in _routablePageTypes)
            {

                Routing.RegisterRoute(pageType.Name, pageType);
            }
            
        }

        private async void FlyoutFooter_Tapped(object sender, TappedEventArgs e)
        {
            await Launcher.OpenAsync("https://www.github.com/hugoMGSung");
        }

        private async void Signout_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.DisplayAlert("Alert", "Signout Clicked!!", "OK");
        }
    }
}
