using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace MVIZadanie1.Model.Templates
{
    public class TemplateDepartment
    {
        public const string TemplatesUrl = "http://mechatronika.cool/noviny/sablony/";

        public string Name { get; set; }
        public List<Template> Templates { get; set; }

        public static List<TemplateDepartment> GetTemplatesFromWeb()
        {
            var htmlString = GkWebClient.DoRequest(TemplatesUrl);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            var entryContentDiv = htmlDocument.DocumentNode.Descendants("div").First(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("entry-content"));
            var entryContentParagraphs = entryContentDiv.Descendants("p").Skip(1);

            var templateDepartments = new List<TemplateDepartment>();
            foreach (var paragraph in entryContentParagraphs)
            {
                var templatesStringArray = paragraph.InnerText.Trim().Split('\n');

                var departmentName = templatesStringArray[0];

                var templates = new List<Template>();
                for (var i = 1; i < templatesStringArray.Length; i++)
                {
                    var splitTemplateNameAndUrl = templatesStringArray[i].Split(':');
                    var template = new Template
                    {
                        Name = splitTemplateNameAndUrl[0],
                        Url = Uri.UnescapeDataString($"{splitTemplateNameAndUrl[1]}:{splitTemplateNameAndUrl[2]}"),
                    };

                    templates.Add(template);
                }

                templateDepartments.Add(new TemplateDepartment
                {
                    Name = departmentName,
                    Templates = templates,
                });
            }

            return templateDepartments;
        }
    }
}
