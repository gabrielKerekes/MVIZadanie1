using System.Net.Http;

namespace MVIZadanie1
{
    // todo: rename
    public class GkWebClient
    {
        // todo: exception handling
        public static string DoRequest(string url)
        {
            var client = new HttpClient();
            return client.GetStringAsync(url).Result;
        }

        public static string GetFileContent(string url)
        {
            return DoRequest(url);
        }
    }
}
