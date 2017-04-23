using System.Collections.Generic;

namespace MVIZadanie1.Model
{
    public class SemesterEvent
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
    }

    public static class SemesterHarmonogram
    {
        private const string HarmonogramFileUrl = "http://www.uamt.fei.stuba.sk/MVI/sites/default/files/noviny/harmonogram_studia.csv";
        
        // todo: nejaku formu cachovania
        public static List<SemesterEvent> GetEventsFromWeb()
        {
            var fileContent = MviWebClient.GetFileContent(HarmonogramFileUrl);
            fileContent = fileContent.Replace("\r\n", "");
            
            var eventList = new List<SemesterEvent>();

            var splitFileContent = fileContent.Split(';');
            for (var i = 3; i < splitFileContent.Length - 3; i += 3)
            {
                var semesterEvent = new SemesterEvent
                {
                    Name = splitFileContent[i],
                    Date = splitFileContent[i + 1],
                    Category = splitFileContent[i + 2],
                };

                eventList.Add(semesterEvent);
            }

            return eventList;
        }
    }
}
