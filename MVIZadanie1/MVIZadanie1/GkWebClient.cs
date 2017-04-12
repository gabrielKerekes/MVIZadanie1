using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVIZadanie1
{
    public class GkWebClient
    {
        public static string DoRequest(string url)
        {
            var client = new HttpClient();
            return client.GetStringAsync(url).Result;
        }

        public static string GetFileContent(string url)
        {
            return DoRequest(url);
        }

        //public static string DownloadImageIfNotCached(string url)
        //{
        //    var client = new WebClient();
        //}
    }
}
