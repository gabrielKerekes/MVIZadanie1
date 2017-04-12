using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MVIZadanie1
{
    // todo: kategorie
    // todo: moznost filtrovat podla kategorie
    // todo: hore vzdy najnovsie clanky
    // todo: ak je v clanku youtube video tak play alebo presmerovat to browser
    public class Article
    {
        public string Title { get; set; }
        public string Extract { get; set; }
        public string Link { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public ImageSource ImageSource { get; set; }

        public Article(string title, string extract, string link, int categoryId, string imageUrl)
        {
            Title = title;
            Extract = extract;
            Link = link;
            CategoryId = categoryId;
            ImageUrl = imageUrl;

            ImageSource = new UriImageSource
            {
                CachingEnabled = true,
                Uri = new Uri(ImageUrl),
            };
        }

        private static string JsonContentToExtract(string jsonContent)
        {
            return jsonContent.Substring(3, 50) + "...";
        }

        public static void DownloadeImage()
        {
        }

        public static IEnumerable<Article> GetArticlesFromWeb()
        {
            var response = GkWebClient.DoRequest("http://mechatronika.cool/noviny/wp-json/wp/v2/posts");

            var root = JsonConvert.DeserializeObject<List<object>>(response);
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

                var extract = JsonContentToExtract(htmlContent);

                var categories = JsonConvert.DeserializeObject<string[]>(objectDict["categories"].ToString());
                var category = int.Parse(categories[0]);

                // todo: get real image
                var imageUrl = "http://mechatronika.cool/noviny/wp-content/uploads/2017/03/Nokia-3310-7-768x432.jpg";

                yield return new Article(title, extract, link, category, imageUrl);
            }
        }
    }
}
