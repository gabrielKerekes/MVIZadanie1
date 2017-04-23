using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace MVIZadanie1.Model
{
    public class SoftwareCategory
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Software> Software { get; set; }
    }

    public class Software
    {
        private const string SoftwareUrl = "http://mechatronika.cool/noviny/softver-pre-studentov/";

        public string Name { get; set; }
        public string Url { get; set; }
        
        public static List<SoftwareCategory> GetSoftwareCategories()
        {
            var htmlString = MviWebClient.DoRequest(SoftwareUrl);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            var topMenu = htmlDocument.GetElementbyId("top-menu");
            if (topMenu == null)
                return new List<SoftwareCategory>();

            var softwareLi = topMenu.Descendants("li").First(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("current_page_item"));
            var softwareCategoriesUl = softwareLi.Descendants("ul").First();
            var softwareCategoryLinks = softwareCategoriesUl.Descendants("li").Select(d => d.Descendants("a").First());
            
            var softwareCategories = new List<SoftwareCategory>();
            foreach (var softwareCategoryLink in softwareCategoryLinks)
            {
                var softwareCategory = new SoftwareCategory
                {
                    Name = softwareCategoryLink.InnerText,
                    Url = softwareCategoryLink.Attributes["href"].Value,
                };

                softwareCategory.Software = GetSoftwareCategorySoftware(softwareCategory.Url);

                softwareCategories.Add(softwareCategory);
            }

            return softwareCategories;
        }

        public static List<Software> GetSoftwareCategorySoftware(string softwareCategoryUrl)
        {
            var entryContentDiv = MviWebClient.GetPageEntryContent(softwareCategoryUrl);

            var softwareList = new List<Software>();
            var softwareStrings = entryContentDiv.InnerText.Trim().Split('\n');
            foreach (var softwareString in softwareStrings)
            {
                var softwareSplitString = softwareString.Split(new [] { "&#8211;" }, StringSplitOptions.None);
                var software = new Software
                {
                    Name = softwareSplitString[0],
                    Url = softwareSplitString[1],
                };

                softwareList.Add(software);
            }

            return softwareList;
        }
    }
}
