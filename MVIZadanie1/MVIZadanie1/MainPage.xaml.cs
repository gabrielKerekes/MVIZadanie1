using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MVIZadanie1
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Article> articles;

        public MainPage()
        {
            InitializeComponent();

            articles = new ObservableCollection<Article>();

            foreach (var article in Article.GetArticlesFromWeb())
            {
                articles.Add(article);
            }

            ArticleListView.ItemsSource = articles;
        }

        private void ArticleListView_OnRefreshing(object sender, EventArgs e)
        {
            
        }

        private void ArticleListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }
    }
}
