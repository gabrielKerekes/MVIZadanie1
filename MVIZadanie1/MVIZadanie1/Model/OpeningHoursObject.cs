using System.Collections.Generic;
using System.Linq;

namespace MVIZadanie1.Model
{
    public class OpeningHoursObject
    {
        private const string OpeningHoursUrl = "http://mechatronika.cool/noviny/otvaracie-hodiny-objektov/";

        public string ObjectName { get; set; }
        public string[] OpeningHoursArray { get; set; }

        public static List<OpeningHoursObject> GetOpeningHoursFromWeb()
        {
            var entryContentDiv = MviWebClient.GetPageEntryContent(OpeningHoursUrl);
            if (entryContentDiv == null)
                return new List<OpeningHoursObject>();

            var entryContentParagraphs = entryContentDiv.Descendants("p");

            var openingHoursList = new List<OpeningHoursObject>();
            foreach (var paragraphs in entryContentParagraphs)
            {
                var openingHoursStringArray = paragraphs.InnerText.Trim().Replace("&#8211;", "-").Split('\n');

                var openingHours = new OpeningHoursObject
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
