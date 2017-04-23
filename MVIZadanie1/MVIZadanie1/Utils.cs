using System;
using System.Globalization;
using Xamarin.Forms;

namespace MVIZadanie1
{
    public static class Utils
    {
        public static long TryParseDate(string dateString, string format = "")
        {
            try
            {
                DateTime date;
                if (string.IsNullOrWhiteSpace(format))
                    date = DateTime.Parse(dateString);
                else
                    date = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

                return (date.Ticks - new DateTime(1970, 1, 1).Ticks) / TimeSpan.TicksPerSecond * 1000;
            }
            catch (Exception)
            {
                // don't care
            }

            return -1;
        }

        public static void CreateReminder(Page page, string title, string description, string dateStart, string dateEnd, string dateFormat = "")
        {
            try
            {
                var startDateUnixT = TryParseDate(dateStart, dateFormat);
                var endDateUnixT = TryParseDate(dateEnd, dateFormat);

                if (startDateUnixT != -1 && endDateUnixT != -1)
                {
                    DependencyService.Get<IReminderCreator>().CreateReminder(title, description, startDateUnixT, endDateUnixT);
                    page.DisplayAlert("Vytvorené", "Vytvorené", "OK");
                }
                else
                {
                    page.DisplayAlert("Zlý dátum", "Zlý dátum", "OK");
                }
            }
            catch (Exception)
            {
                // don't care
            }
        }
    }
}
