using MVIZadanie1.Elements;
using MVIZadanie1.Model;
using Xamarin.Forms;

namespace MVIZadanie1.Pages
{
    public partial class OpeningHoursPage
    {
        private bool appeared;
        private int selectedOpeningHoursObjectIndex;

        public OpeningHoursPage()
        {
            InitializeComponent();
            Title = "Objekty";

            selectedOpeningHoursObjectIndex = -1;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (appeared)
                return;

            var objectButtonGroup = CreateObjectButtonGroupView();
            MainStackLayout.Children.Add(objectButtonGroup);

            appeared = true;
        }

        private ButtonGroupView CreateObjectButtonGroupView()
        {
            var objectButtonGroupView = new ButtonGroupView();
            var openingHoursList = OpeningHoursObject.GetOpeningHoursFromWeb();
            foreach (var openingHoursObject in openingHoursList)
            {
                var objectButton = new Button
                {
                    Text = openingHoursObject.ObjectName,
                };

                objectButton.Clicked += (sender, args) =>
                {
                    ObjectButton_Clicked(objectButtonGroupView, openingHoursList, openingHoursObject, objectButton);
                };

                objectButtonGroupView.AddButton(objectButton);
            }

            return objectButtonGroupView;
        }

        private void ObjectButton_Clicked(ButtonGroupView objectButtonGroupView, System.Collections.Generic.List<OpeningHoursObject> openingHoursList, OpeningHoursObject openingHoursObject, Button objectButton)
        {
            if (selectedOpeningHoursObjectIndex == openingHoursList.FindIndex(o => o == openingHoursObject))
            {
                MainStackLayout.Children.Clear();
                MainStackLayout.Children.Add(objectButtonGroupView);

                objectButtonGroupView.UnselectButton(objectButton);
                selectedOpeningHoursObjectIndex = -1;
            }
            else
            {
                MainStackLayout.Children.Clear();
                MainStackLayout.Children.Add(objectButtonGroupView);

                objectButtonGroupView.SelectButton(objectButton);
                ViewOpeningHoursForObject(openingHoursObject);
                selectedOpeningHoursObjectIndex = openingHoursList.FindIndex(o => o == openingHoursObject);
            }
        }

        private void ViewOpeningHoursForObject(OpeningHoursObject openingHoursObject)
        {
            var openingHoursObjectNameLabel = new Label
            {
                FontSize = 20,
                Text = openingHoursObject.ObjectName
            };

            MainStackLayout.Children.Add(openingHoursObjectNameLabel);

            foreach (var openingHour in openingHoursObject.OpeningHoursArray)
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
