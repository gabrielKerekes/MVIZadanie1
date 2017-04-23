using System.Threading.Tasks;
using Xamarin.Forms;

namespace MVIZadanie1.Pages
{
    public partial class SplashScreenPage
    {
        public SplashScreenPage()
        {
            InitializeComponent();

            var logoImage = new Image
            {
                Aspect = Aspect.AspectFit,
                HeightRequest = 300,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Source = ImageSource.FromResource("MVIZadanie1.Images.spojene.png")
            };

            MainStackLayout.Children.Add(logoImage);

            WaitAndRedirect();
        }
        
        private async void WaitAndRedirect()
        {
            var page = new MainPage();

            await Task.Delay(2500);

            await Navigation.PushModalAsync(page);
        }
    }
}
