using System;
using System.Collections.ObjectModel;
using MVIZadanie1.Model;
using Xamarin.Forms;

namespace MVIZadanie1.Pages
{
    public partial class EventPage
    {
        private bool appeared;

        private ObservableCollection<Event> events;

        public EventPage()
        {
            InitializeComponent();
            // todo: resources
            Title = "Udalosti";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (appeared)
                return;
            
            LoadContent();

            appeared = true;
        }

        private void LoadContent()
        {
            events = new ObservableCollection<Event>();
            foreach (var e in Event.GetAllEventsFromWeb())
            {
                events.Add(e);
            }

            EventListView.ItemsSource = events;
        }

        private void EventListView_OnRefreshing(object sender, EventArgs e)
        {
            
        }

        private void EventListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var ev = (Event) e.Item;

            Utils.CreateReminder(this, ev.Name, ev.Place, ev.StartDateString, ev.EndDateString);
        }
    }
}
