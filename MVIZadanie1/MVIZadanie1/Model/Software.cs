using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace MVIZadanie1.Model
{
    // todo: GABO - mergnut software categ a software
    public class SoftwareCategory
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Software> Software { get; set; }
    }

    public class Software
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public const string SoftwareUrl = "http://mechatronika.cool/noviny/softver-pre-studentov/";

        // todo: GABO - refactor
        public static List<SoftwareCategory> GetSoftwareCategories()
        {
            var htmlString = GkWebClient.DoRequest(SoftwareUrl);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            var topMenu = htmlDocument.GetElementbyId("top-menu");
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
            var htmlString = GkWebClient.DoRequest(softwareCategoryUrl);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            var entryContentDiv = htmlDocument.DocumentNode.Descendants("div").First(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("entry-content"));

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
