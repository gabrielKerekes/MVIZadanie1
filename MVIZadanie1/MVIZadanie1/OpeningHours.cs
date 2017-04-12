using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MVIZadanie1
{
    public class OpeningHours
    {
        public const string OpeningHoursUrl = "http://mechatronika.cool/noviny/otvaracie-hodiny-objektov/";

        public string ObjectName { get; set; }
        public string[] OpeningHoursArray { get; set; }

        public static List<OpeningHours> GetOpeningHoursFromWeb()
        {
            var htmlString = GkWebClient.DoRequest(OpeningHoursUrl);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            var entryContentDiv = htmlDocument.DocumentNode.Descendants("div").First(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("entry-content"));
            var entryContentParagraphs = entryContentDiv.Descendants("p");

            var openingHoursList = new List<OpeningHours>();
            foreach (var paragraphs in entryContentParagraphs)
            {
                var openingHoursStringArray = paragraphs.InnerText.Trim().Split('\n');

                var openingHours = new OpeningHours
                {
                    ObjectName = openingHoursStringArray[0],
                    OpeningHoursArray = openingHoursStringArray.Skip(1).ToArray(),
                };

                openingHoursList.Add(openingHours);
            }

            return openingHoursList;
        }
    }
}
