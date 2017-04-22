using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MVIZadanie1.Model;
using Xamarin.Forms;

namespace MVIZadanie1.Pages
{
    // todo: nastavenie pripomienky
    public partial class FinalsSchedulePage
    {
        private bool appeared;

        private List<FinalExam> allFinalExams;

        public FinalsSchedulePage()
        {
            InitializeComponent();
            // todo: resources
            Title = "Skúšky";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (appeared)
                return;

            allFinalExams = new List<FinalExam>();
            foreach (var finalExam in FinalsSchedule.GetFinalsSchedule())
            {
                allFinalExams.Add(finalExam);
            }
            
            FinalsScheduleListView.ItemsSource = new ObservableCollection<FinalExam>(allFinalExams);

            appeared = true;
        }

        private void FinalsSearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            FinalsScheduleListView.BeginRefresh();

            FinalsScheduleListView.ItemsSource = new ObservableCollection<FinalExam>(GetFilteredFinalExams(e.NewTextValue));

            FinalsScheduleListView.EndRefresh();
        }

        private IEnumerable<FinalExam> GetFilteredFinalExams(string text)
        {
            text = text.ToLower();

            return allFinalExams.Where(fe => fe.Subject.ToLower().Contains(text)
                                            || fe.Code.ToLower().Contains(text)
                                            || fe.Department.ToLower().Contains(text)
                                            || fe.FirstRoom.ToLower().Contains(text)
                                            || fe.FirstDate.ToLower().Contains(text)
                                            || fe.FirstTime.ToLower().Contains(text)
                                            || fe.SecondRoom.ToLower().Contains(text)
                                            || fe.SecondDate.ToLower().Contains(text)
                                            || fe.SecondTime.ToLower().Contains(text));
        }

        // todo: tu skoncene
        private void FinalsScheduleListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var finalExam = (FinalExam) e.Item;

            var firstStartDateJavaMilis = TryParseDate(finalExam.FirstDate);

            if (firstStartDateJavaMilis != -1)
            { 
                DependencyService.Get<IReminderCreator>().CreateReminder($"Skuska {finalExam.Subject}", $"Miestnosť {finalExam.FirstRoom}", firstStartDateJavaMilis);
                DisplayAlert("Vytvorené", "Vytvorené", "OK");
            }
            else
                DisplayAlert("Zlý dátum", "Zlý dátum", "OK");

            var secondStartDateJavaMilis = TryParseDate(finalExam.SecondDate);

            if (secondStartDateJavaMilis != -1)
                DependencyService.Get<IReminderCreator>().CreateReminder($"Skuska {finalExam.Subject}", $"Miestnosť {finalExam.SecondRoom}", secondStartDateJavaMilis);
            else
                DisplayAlert("Druhý Zlý dátum", "Druhý Zlý dátum", "OK");
        }

        private long TryParseDate(string dateString)
        {
            try
            {
                var date = DateTime.Parse(dateString);
                return date.Subtract(new DateTime(1970, 1, 1)).Ticks;
            }
            catch (Exception e)
            {
                
            }

            return -1;
        }
    }
}
