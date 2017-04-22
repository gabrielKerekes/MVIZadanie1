using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIZadanie1.Model
{
    public class Event
    {
        private const string EventApiUrl = "http://mvizadanie1dbservice.azurewebsites.net/api/event";

        public string Name { get; set; }
        public string Description { get; set; }
        public string StartDateString { get; set; }
        public DateTime StartDate { get; set; }
        public string EndDateString { get; set; }
        public DateTime EndDate { get; set; }
        public string Place { get; set; }

        public static List<Event> GetAllEventsFromWeb()
        {
            var eventsString = GkWebClient.DoRequest(EventApiUrl);

            var eventsStringSplit = eventsString.Substring(1, eventsString.Length - 3).Split(';');

            var events = new List<Event>();
            foreach (var eventString in eventsStringSplit)
            {
                var e = new Event();

                var eventStringSplit = eventString.Split(new [] { "$$$" }, StringSplitOptions.None);

                e.Name = eventStringSplit[3];
                e.Description = eventStringSplit[5];
                e.StartDateString = eventStringSplit[7];
                e.StartDate = DateTime.Parse(e.StartDateString);
                e.EndDateString = eventStringSplit[9];
                e.EndDate = DateTime.Parse(e.EndDateString);
                e.Place = eventStringSplit[13];

                events.Add(e);
            }

            return events;
        }
    }
}
