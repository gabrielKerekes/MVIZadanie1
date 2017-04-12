using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MVIZadanie1
{
    public class Template
    {
        public const string TemplatesUrl = "http://mechatronika.cool/noviny/sablony/";

        public string Department { get; set; }
        public string[] TemplatesStringArray { get; set; }

        public static List<Template> GetTemplatesFromWeb()
        {
            var htmlString = GkWebClient.DoRequest(TemplatesUrl);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            var entryContentDiv = htmlDocument.DocumentNode.Descendants("div").First(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("entry-content"));
            var entryContentParagraphs = entryContentDiv.Descendants("p").Skip(1);

            var temeplates = new List<Template>();
            foreach (var paragraph in entryContentParagraphs)
            {
                var templatesStringArray = paragraph.InnerText.Trim().Split('\n');

                var template = new Template
                {
                    Department = templatesStringArray[0],
                    TemplatesStringArray = templatesStringArray.Skip(1).ToArray(),
                };

                temeplates.Add(template);
            }

            return temeplates;
        }
    }
}
