namespace MVIZadanie1
{
    public interface IReminderCreator
    {
        void CreateReminder(string title, string description, long startDateUnixT, long endDateUnixT);
    }
}
