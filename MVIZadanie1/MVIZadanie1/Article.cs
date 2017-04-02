using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MVIZadanie1
{
    public class Article
    {
        public string Title { get; set; }
        public string Extract { get; set; }
        public string Link { get; set; }
        public int CategoryId { get; set; }

        public Article(string title, string extract, string link, int categoryId)
        {
            Title = title;
            Extract = extract;
            Link = link;
            CategoryId = categoryId;
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
                
                yield return new Article(title, extract, link, category);
            }
        }
    }
}
