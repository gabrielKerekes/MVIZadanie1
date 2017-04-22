using System.Collections.Generic;

namespace MVIZadanie1.Model
{
    public class FinalExam
    {
        // todo: rename
        public string Department { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public string FirstDate { get; set; }
        public string FirstTime { get; set; }
        public string FirstRoom { get; set; }
        public string SecondDate { get; set; }
        public string SecondTime { get; set; }
        public string SecondRoom { get; set; }

        public string SubjectAndCodeAndDepartment => $"{Subject} ({Code}) - {Department}";
        public string FirstTry => $"Čas: {FirstDate} Beh: {FirstTime} Miestnosť: {FirstRoom}";
        public string SecondTry => $"Čas: {SecondDate} Beh: {SecondTime} Miestnosť: {SecondRoom}";
    }

    // todo: rename
    public class FinalsSchedule
    {
        public const string FinalsScheduleFileUrl = "http://www.uamt.fei.stuba.sk/MVI/sites/default/files/noviny/Terminy_skusok_FEI_ZS_2016_17f.csv";
        
        public static IEnumerable<FinalExam> GetFinalsSchedule()
        {
            var fileContent = GkWebClient.GetFileContent(FinalsScheduleFileUrl);
            fileContent = fileContent.Replace("\r", "");

            var i = 0;
            var splitLines = fileContent.Split('\n');
            foreach (var splitLine in splitLines)
            {
                // leave out first (header) and the last (empty line)
                if (i == 0 || i == splitLines.Length - 1)
                {
                    i++;
                    continue;
                }

                var splitCategories = splitLine.Split(';');
                
                var finalExam = new FinalExam
                {
                    Department = splitCategories[0],
                    Code = splitCategories[1],
                    Subject = splitCategories[2],
                    FirstDate = splitCategories[3],
                    FirstTime = splitCategories[4],
                    FirstRoom = splitCategories[5],
                    SecondDate = splitCategories[6],
                    SecondTime = splitCategories[7],
                    SecondRoom = splitCategories[8],
                };

                i++;

                yield return finalExam;
            }
        }
    }
}
