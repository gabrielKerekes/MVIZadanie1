using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MVIZadanie1
{
    // todo: vyhladavanie podla predmetu
    // todo: nastavenie pripomienky
    public partial class FinalsSchedulePage : ContentPage
    {
        private ObservableCollection<FinalExam> finalExams;

        public FinalsSchedulePage()
        {
            InitializeComponent();
            // todo: resources
            Title = "Rozvrh skúšok";

            finalExams = new ObservableCollection<FinalExam>();

            foreach (var finalExam in FinalsSchedule.GetFinalsSchedule())
            {
                finalExams.Add(finalExam);
            }

            FinalsScheduleListView.ItemsSource = finalExams;
        }
    }
}
