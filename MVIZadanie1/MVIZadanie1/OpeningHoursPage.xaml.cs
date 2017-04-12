using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MVIZadanie1
{
    public partial class OpeningHoursPage : ContentPage
    {
        public OpeningHoursPage()
        {
            InitializeComponent();
            Title = "Otváracie hodiny objektov";

            var openingHoursList = OpeningHours.GetOpeningHoursFromWeb();

            foreach (var openingHours in openingHoursList)
            {
                var openingHoursObjectNameLabel = new Label
                {
                    FontSize = 20,
                    Text = openingHours.ObjectName
                };

                MainStackLayout.Children.Add(openingHoursObjectNameLabel);

                foreach (var openingHour in openingHours.OpeningHoursArray)
                {
                    var openingHourLabel = new Label
                    {
                        FontSize = 16,
                        Text = openingHour,
                    };

                    MainStackLayout.Children.Add(openingHourLabel);
                }
            }
        }
    }
}
