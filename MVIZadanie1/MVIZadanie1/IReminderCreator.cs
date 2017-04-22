using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIZadanie1
{
    // todo: rename
    public interface IReminderCreator
    {
        void CreateReminder(string title, string description, long startJavaMilis);
    }
}
