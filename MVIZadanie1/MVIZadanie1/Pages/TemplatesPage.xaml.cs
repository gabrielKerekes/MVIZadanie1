using System;
using System.Collections.Generic;
using MVIZadanie1.Model.Templates;
using Xamarin.Forms;

namespace MVIZadanie1.Pages
{
    public partial class TemplatesPage
    {
        private bool appeared;

        public TemplatesPage()
        {
            InitializeComponent();
            Title = "Šablóny";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (appeared)
                return;

            var templates = TemplateDepartment.GetTemplatesFromWeb();
            ViewTemplates(templates);

            appeared = true;
        }

        private void ViewTemplates(List<TemplateDepartment> templateDepartments)
        {
            foreach (var templateDepartment in templateDepartments)
            {
                var templateLabel = new Label
                {
                    FontSize = 20,
                    Text = templateDepartment.Name
                };

                MainStackLayout.Children.Add(templateLabel);

                foreach (var template in templateDepartment.Templates)
                {
                    var name = template.Name;
                    var url = template.Url;

                    var templateStringLabel = new Label
                    {
                        FontSize = 16,
                        Text = $"{name}: {url}",
                        TextColor = Color.Blue,
                    };

                    templateStringLabel.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() =>
                        {
                            Device.OpenUri(new Uri(url));
                        })
                    });

                    MainStackLayout.Children.Add(templateStringLabel);
                }
            }
        }
    }
}
