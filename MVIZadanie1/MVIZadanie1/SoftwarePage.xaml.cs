using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MVIZadanie1
{
    public partial class SoftwarePage : ContentPage
    {
        public SoftwarePage()
        {
            InitializeComponent();
            Title = "Softvér pre študentov";

            var softwareCategories = Software.GetSoftwareCategories();

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
                    var softwareLabel = new Label
                    {
                        FontSize = 16,
                        Text = software,
                    };

                    MainStackLayout.Children.Add(softwareLabel);
                }
            }
        }

        private void SoftwareListView_OnRefreshing(object sender, EventArgs e)
        {
            
        }

        private void SoftwareListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }
    }
}
