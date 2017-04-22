using System.Threading.Tasks;
using Xamarin.Forms;

namespace MVIZadanie1.Pages
{
    public partial class SplashScreenPage
    {
        public SplashScreenPage()
        {
            InitializeComponent();

            var feiLogoImage = new Image
            {
                Aspect = Aspect.AspectFit,
                HeightRequest = 150,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Source = ImageSource.FromResource("MVIZadanie1.Images.feiLogo.png")
            };

            var mechatronicsLogoImage = new Image
            {
                Aspect = Aspect.AspectFit,
                HeightRequest = 50,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Source = ImageSource.FromResource("MVIZadanie1.Images.mechatronikaLogo.png")
            };

            MainStackLayout.Children.Add(feiLogoImage);
            MainStackLayout.Children.Add(mechatronicsLogoImage);

            WaitAndRedirect();
        }

        // todo: zfunkcnit
        private async void WaitAndRedirect()
        {
            //await Task.Delay(5000);

            await Navigation.PushModalAsync(new MainPage());
        }
    }
}
