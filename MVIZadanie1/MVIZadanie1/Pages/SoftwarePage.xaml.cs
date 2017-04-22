using System.Collections.Generic;
using MVIZadanie1.Elements;
using MVIZadanie1.Model;
using Xamarin.Forms;

namespace MVIZadanie1.Pages
{
    public partial class SoftwarePage
    {
        private bool appeared;

        public SoftwarePage()
        {
            InitializeComponent();
            Title = "Softvér";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (appeared)
                return;

            AddSoftwareToView(Software.GetSoftwareCategories());

            appeared = true;
        }

        private void AddSoftwareToView(List<SoftwareCategory> softwareCategories)
        {
            foreach (var softwareCategory in softwareCategories)
            {
                var categoryNameLabel = new Label
                {
                    FontSize = 20,
                    Text = softwareCategory.Name
                };

                MainStackLayout.Children.Add(categoryNameLabel);

                foreach (var software in softwareCategory.Software)
                {
                    MainStackLayout.Children.Add(new LinkLabel(software.Name, software.Url));
                }
            }

        }
    }
}
