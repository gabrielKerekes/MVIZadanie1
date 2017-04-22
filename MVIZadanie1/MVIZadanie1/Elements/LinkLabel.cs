using System;
using Xamarin.Forms;

namespace MVIZadanie1.Elements
{
    public class LinkLabel : Label
    {
        public string Title { get; set; }
        public string Url { get; set; }

        public LinkLabel(string title, string url)
        {
            Title = title;
            Url = url;
            
            FontSize = 16;
            Text = $"{Title}: {Url}";
            TextColor = Color.Blue;

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Device.OpenUri(new Uri(Url));
                })
            });
        }
    }
}
