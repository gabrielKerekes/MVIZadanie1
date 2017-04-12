using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MVIZadanie1
{
    public partial class SemesterHarmonogramPage : ContentPage
    {
        private ObservableCollection<SemesterEvent> semesterEvents;
        
        public SemesterHarmonogramPage()
        {
            InitializeComponent();
            // todo: resources
            Title = "Harmonogram";

            semesterEvents = new ObservableCollection<SemesterEvent>();
            
            foreach (var semesterEvent in SemesterHarmonogram.GetEventsFromWeb())
            {
                semesterEvents.Add(semesterEvent);
            }

            SemesterHarmonogramListView.ItemsSource = semesterEvents;
        }

        private void SemesterHarmonogramListView_OnRefreshing(object sender, EventArgs e)
        {
            
        }

        private void SemesterHarmonogramListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }
    }
}
