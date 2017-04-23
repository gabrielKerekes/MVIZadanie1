using System;
using System.Collections.ObjectModel;
using MVIZadanie1.Model;
using Xamarin.Forms;

namespace MVIZadanie1.Pages
{
    public partial class SemesterHarmonogramPage
    {
        private bool appeared;
        private ObservableCollection<SemesterEvent> semesterEvents;
        
        public SemesterHarmonogramPage()
        {
            InitializeComponent();
            // todo: resources
            Title = "Semester";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (appeared)
                return;

            semesterEvents = new ObservableCollection<SemesterEvent>();
            foreach (var semesterEvent in SemesterHarmonogram.GetEventsFromWeb())
            {
                semesterEvents.Add(semesterEvent);
            }

            SemesterHarmonogramListView.ItemsSource = semesterEvents;

            appeared = true;
        }
    }
}
