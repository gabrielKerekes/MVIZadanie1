using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MVIZadanie1
{
    public partial class TemplatesPage : ContentPage
    {
        public TemplatesPage()
        {
            InitializeComponent();
            Title = "Šablóny";

            var templates = Template.GetTemplatesFromWeb();

            foreach (var template in templates)
            {
                var templateLabel = new Label
                {
                    FontSize = 20,
                    Text = template.Department
                };

                MainStackLayout.Children.Add(templateLabel);

                foreach (var templateString in template.TemplatesStringArray)
                {
                    var templateStringLabel = new Label
                    {
                        FontSize = 16,
                        Text = templateString,
                    };

                    MainStackLayout.Children.Add(templateStringLabel);
                }
            }
        }
    }
}
