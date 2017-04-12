using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MVIZadanie1
{
    public partial class ArticlesPage : ContentPage
    {
        private ObservableCollection<Article> articles;

        public ArticlesPage()
        {
            InitializeComponent();
            // todo: resources
            Title = "Articles";

            articles = new ObservableCollection<Article>();

            foreach (var article in Article.GetArticlesFromWeb())
            {
                articles.Add(article);
            }

            ArticleListView.ItemsSource = articles;
        }

        private void ArticleListView_OnRefreshing(object sender, EventArgs e)
        {
            // todo: refresh - wodnload from web again
        }

        private void ArticleListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            // todo: go to article
        }
    }
}
