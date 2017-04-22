using MVIZadanie1.Pages;
using Xamarin.Forms;

namespace MVIZadanie1
{
    public class App : Application
    {
        public App()
        {
            MainPage = new SplashScreenPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
