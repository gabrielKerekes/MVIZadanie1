using System;
using MVIZadanie1.Model;
using Xamarin.Forms;

namespace MVIZadanie1.Pages
{
    public partial class SingleArticlePage
    {
        public SingleArticlePage(Article article)
        {
            InitializeComponent();

            var label = new Label
            {
                Text = article.Title,
                FontSize = 22,
            };

            MainStackLayout.Children.Add(label);

            article.ParseHtmlContent();

            foreach (var articleElement in article.ArticleElements)
            {
                var elementType = articleElement.Item1;
                var elementString = articleElement.Item2;

                // todo: refactor
                switch (elementType)
                {
                    case ArticleElementType.Video:
                        var webViewSource = new HtmlWebViewSource {Html = elementString};

                        var webView = new WebView
                        {
                            Source = webViewSource,
                            WidthRequest = 300,
                            HeightRequest = 150,
                        };

                        MainStackLayout.Children.Add(webView);
                        break;

                    case ArticleElementType.Image:
                        var imageSource = ImageSource.FromUri(new Uri(elementString));
                        var image = new Image
                        {
                            Source = imageSource,
                        };

                        MainStackLayout.Children.Add(image);
                        break;

                    case ArticleElementType.Paragraph:
                        var lineLabel = new Label
                        {
                            Text = elementString,
                        };

                        MainStackLayout.Children.Add(lineLabel);
                        break;

                    default:
                        // todo: error or smth
                        break;
                }
            }

            // todo: remove if not needed
            // todo: refactor .. aj Article.cs
            //foreach (var line in article.TextLines)
            //{
            //}

            //foreach (var imageUrl in article.ImageUrls)
            //{
            //}

            //// todo: skusit dat spinner pri nacitavani
            //foreach (var videoUrl in article.VideoUrls)
            //{
            //}

            var backButton = new Button
            {
                Text = "Späť",
            };

            backButton.Clicked += (sender, args) =>
            {
                Navigation.PopModalAsync();
            };

            MainStackLayout.Children.Add(backButton);
        }
    }
}
