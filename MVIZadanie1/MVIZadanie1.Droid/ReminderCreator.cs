using Android.App;
using Android.Content;
using Android.Provider;
using MVIZadanie1.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ReminderCreator))]
namespace MVIZadanie1.Droid
{
    public class ReminderCreator : IReminderCreator
    {
        private readonly string[] calendarsProjection =
        {
            CalendarContract.Calendars.InterfaceConsts.Id,
            CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
            CalendarContract.Calendars.InterfaceConsts.AccountName
        };

        public void CreateReminder(string title, string description, long startDateUnixT, long endDateUnixT)
        {
            var calendarsUri = CalendarContract.Calendars.ContentUri;
            var cursor = Application.Context.ContentResolver.Query(calendarsUri, calendarsProjection, null, null, null);
            cursor.MoveToFirst();
            var calendarId = cursor.GetInt(cursor.GetColumnIndex(calendarsProjection[0]));

            var eventValues = new ContentValues();
            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, calendarId);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, title);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, description);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, startDateUnixT);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, endDateUnixT);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");

            Application.Context.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
        }
    }
}