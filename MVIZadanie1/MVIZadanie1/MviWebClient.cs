using System;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;

namespace MVIZadanie1
{
    public class MviWebClient
    {
        public static string DoRequest(string url)
        {
            try
            {
                var client = new HttpClient();
                return client.GetStringAsync(url).Result;
            }
            catch (Exception)
            {
                // don't care
            }

            return "";
        }

        public static HtmlNode GetPageEntryContent(string url)
        {
            try
            {
                var htmlString = DoRequest(url);

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlString);

                return htmlDocument.DocumentNode.Descendants("div").First(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("entry-content"));
            }
            catch (Exception)
            {
                // don't care
            }

            return null;
        }

        public static string GetFileContent(string url)
        {
            return DoRequest(url);
        }
    }
}
