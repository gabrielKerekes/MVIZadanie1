using MVIZadanie1.Model;
using Xamarin.Forms;

namespace MVIZadanie1.Pages
{
    public partial class SourcesPage
    {
        private bool appeared;

        public SourcesPage()
        {
            InitializeComponent();
            // todo: resources
            Title = "Zdroje";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (appeared)
                return;

            var webViewSource = new HtmlWebViewSource { Html = Source.GetSourcesHtml() };
            SourcesWebView.Source = webViewSource;

            appeared = true;
        }
    }
}
