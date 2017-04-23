namespace MVIZadanie1.Pages
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            Children.Add(new ArticlesPage());
            Children.Add(new TemplatesPage());
            Children.Add(new OpeningHoursPage());
            Children.Add(new SourcesPage());
            Children.Add(new EventPage());
            Children.Add(new SoftwarePage());
            Children.Add(new SemesterHarmonogramPage());
            Children.Add(new FinalsSchedulePage());
        }
    }
}
