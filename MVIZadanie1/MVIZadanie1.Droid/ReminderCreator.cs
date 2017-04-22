using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using MVIZadanie1.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ReminderCreator))]
namespace MVIZadanie1.Droid
{
    public class ReminderCreator : IReminderCreator
    {
        private string[] calendarsProjection =
        {
            CalendarContract.Calendars.InterfaceConsts.Id,
            CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
            CalendarContract.Calendars.InterfaceConsts.AccountName
        };

        public void CreateReminder(string title, string description, long startJavaMilis)
        {
            var calendarsUri = CalendarContract.Calendars.ContentUri;
            var cursor = Application.Context.ContentResolver.Query(calendarsUri, calendarsProjection, null, null, null);
            cursor.MoveToFirst();
            var calendarId = cursor.GetInt(cursor.GetColumnIndex(calendarsProjection[0]));

            ContentValues eventValues = new ContentValues();
            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, calendarId);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, title);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, description);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, startJavaMilis);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, startJavaMilis);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");

            Application.Context.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
        }
    }
}