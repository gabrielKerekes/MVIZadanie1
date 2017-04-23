using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MVIZadanie1.Model
{
    public enum ArticleElementType
    {
        Paragraph, Image, Video,
    }
    
    public class Article
    {
        private const int DefaultExcerptLength = 30;

        public static readonly Dictionary<int, string> CategoryNameMap = new Dictionary<int, string>
        {
             // todo: doplnit rady pre studentov
            {8, "Správy zo sveta FEI"},
            {14, "Technické novinky"},
            {1, "Celebrity"}
        };

        private bool parsed;

        public string Title { get; set; }
        public string Text { get; set; }
        public string Extract { get; set; }
        public string Link { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public ImageSource ImageSource { get; set; }

        public List<Tuple<ArticleElementType, string>> ArticleElements { get; set; }

        public Article(string title, string text, string extract, string link, int categoryId, string imageUrl)
        {
            parsed = false;

            Title = title;
            Text = text;
            Extract = extract;
            Link = link;
            CategoryId = categoryId;
            CategoryName = CategoryNameMap[CategoryId];
            ImageUrl = imageUrl;

            ImageSource = new UriImageSource
            {
                CachingEnabled = true,
                Uri = new Uri(ImageUrl),
            };

            ArticleElements = new List<Tuple<ArticleElementType, string>>();
        }

        private static string JsonContentToExcerpt(string jsonContent)
        {
            var excerptLength = DefaultExcerptLength;
            if (jsonContent.Length < DefaultExcerptLength)
                excerptLength = jsonContent.Length;

            return $"{jsonContent.Substring(3, excerptLength)}...";
        }

        public static List<Article> GetArticlesFromWeb()
        {
            var response = MviWebClient.DoRequest("http://mechatronika.cool/noviny/wp-json/wp/v2/posts");

            var root = JsonConvert.DeserializeObject<List<object>>(response);
            if (root == null)
                return new List<Article>();

            var articleList = new List<Article>();
            foreach (var obj in root)
            {
                var objectJson = obj.ToString();
                // remove extra { and } at beginning and end of objectJson
                objectJson = objectJson.Substring(0, objectJson.Length);
                var objectDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(objectJson);

                var link = (string)objectDict["link"];
                var titleDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(objectDict["title"].ToString());
                var title = titleDict["rendered"].ToString();

                var contentDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(objectDict["content"].ToString());
                var htmlContent = contentDict["rendered"].ToString();
                
                var excerpt = JsonContentToExcerpt(htmlContent);

                var categories = JsonConvert.DeserializeObject<string[]>(objectDict["categories"].ToString());
                var category = int.Parse(categories[0]);

                var featuredMedia = (long) objectDict["featured_media"];

                string imageUrl;
                if (featuredMedia == 0)
                {
                    imageUrl = "http://kf.elf.stuba.sk/~bioelektronika/fei_logo.jpg";
                }
                else
                {
                    var imageObjectJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(MviWebClient.DoRequest($"http://mechatronika.cool/noviny/wp-json/wp/v2/media/{featuredMedia}"));
                    var guidDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(imageObjectJson["guid"].ToString());
                    imageUrl = guidDict["rendered"].ToString();
                }

                articleList.Add(new Article(title, htmlContent, excerpt, link, category, imageUrl));
            }

            return articleList;
        }

        public void ParseHtmlContent()
        {
            if (parsed)
                return;

            var document = new HtmlDocument();
            document.LoadHtml(Text);
            var nodes = document.DocumentNode.ChildNodes;
            
            foreach (var paragraph in nodes.Where(n => n.Name == "p"))
            {
                try
                {
                    if (paragraph.ChildNodes.Any(n => n.Name == "iframe"))
                    {
                        // todo: netreba nejaky error handling?
                        var iframe = paragraph.ChildNodes.First(n => n.Name == "iframe");
                        iframe.SetAttributeValue("width", "300");
                        iframe.SetAttributeValue("height", "150");

                        ArticleElements.Add(Tuple.Create(ArticleElementType.Video, iframe.OuterHtml));
                    }
                    else if (paragraph.ChildNodes.Any(n => n.Name == "img"))
                    {
                        var imageUrl = paragraph.ChildNodes.First(n => n.Name == "img").GetAttributeValue("src", "");
                        if (!string.IsNullOrWhiteSpace(imageUrl))
                            ArticleElements.Add(Tuple.Create(ArticleElementType.Image, imageUrl));
                    }
                    else
                    {
                        ArticleElements.Add(Tuple.Create(ArticleElementType.Paragraph, paragraph.InnerHtml));
                    }
                }
                catch (Exception)
                {
                    // don't care
                }
            }

            parsed = true;
        }
    }
}
