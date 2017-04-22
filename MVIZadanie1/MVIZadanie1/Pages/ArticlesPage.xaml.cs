using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MVIZadanie1.Model;
using Xamarin.Forms;

namespace MVIZadanie1.Pages
{
    public partial class ArticlesPage
    {
        private bool appeared;

        private List<Article> allArticles;
        private ObservableCollection<Article> filteredArticles;
        private int categoryFilter;

        public ArticlesPage()
        {
            InitializeComponent();
            // todo: resources
            Title = "Články";

            categoryFilter = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (appeared)
                return;

            var categoryButtonGroupView = CreateCategoryButtonGroup();
            MainStackLayout.Children.Insert(0, categoryButtonGroupView);

            LoadContent();

            appeared = true;
        }

        private void LoadContent()
        {
            allArticles = new List<Article>();
            filteredArticles = new ObservableCollection<Article>();
            foreach (var article in Article.GetArticlesFromWeb())
            {
                allArticles.Add(article);
                filteredArticles.Add(article);
            }

            ArticleListView.ItemsSource = filteredArticles;
        }

        private ButtonGroupView CreateCategoryButtonGroup()
        {
            var categoryButtonGroup = new ButtonGroupView();
            foreach (var category in Article.CategoryNameMap)
            {
                var categoryButton = new Button
                {
                    Text = category.Value,
                };

                categoryButton.Clicked += (sender, args) =>
                {
                    CategoryButton_Clicked(categoryButtonGroup, category, categoryButton);
                };

                categoryButtonGroup.AddButton(categoryButton);
            }

            return categoryButtonGroup;
        }

        private void CategoryButton_Clicked(ButtonGroupView categoryButtonGroup, KeyValuePair<int, string> category, Button categoryButton)
        {
            if (categoryFilter == category.Key)
            {
                categoryFilter = 0;
                categoryButtonGroup.UnselectButton(categoryButton);
            }
            else
            {
                categoryFilter = category.Key;
                categoryButtonGroup.SelectButton(categoryButton);
            }

            UpdateArticleList();
        }

        private void UpdateArticleList()
        {
            filteredArticles = new ObservableCollection<Article>(allArticles.Where(a => categoryFilter == 0 || a.CategoryId == categoryFilter));
            ArticleListView.ItemsSource = filteredArticles;
        }

        private void ArticleListView_OnRefreshing(object sender, EventArgs e)
        {
            LoadContent();
        }

        private void ArticleListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var article = (Article) e.Item;
            Navigation.PushModalAsync(new SingleArticlePage(article));
        }
    }
}
