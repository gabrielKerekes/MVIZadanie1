using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIZadanie1
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
    }

    // todo: rename
    public class FinalsSchedule
    {
        public const string FinalsScheduleFileUrl = "http://www.uamt.fei.stuba.sk/MVI/sites/default/files/noviny/Terminy_skusok_FEI_ZS_2016_17f.csv";
        
        public static List<FinalExam> GetFinalsSchedule()
        {
            var fileContent = GkWebClient.GetFileContent(FinalsScheduleFileUrl);
            fileContent = fileContent.Replace("\r", "");

            var examList = new List<FinalExam>();

            int i = 0;
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

                examList.Add(finalExam);

                i++;
            }

            return examList;
        }
    }
}
