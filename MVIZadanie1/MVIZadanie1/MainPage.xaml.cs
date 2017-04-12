using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MVIZadanie1;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MVIZadanie1
{
    // todo: sablony 
    // todo: otvaracie hodiny
    // todo: zdroje informacii
     //todo: kalendar s udalostami
     //todo: zoznam softveru
     //todo: harmonogram studia
     //todo: rozvrh skusok
     //todo: splash screen
     
    // todo: vycistit stringt od &#8211 alebo to prerobit na pomlcku alebo co
    public partial class MainPage : TabbedPage
    {
        //todo: move article list view to another activity maybe
        public MainPage()
        {
            InitializeComponent();
            Children.Add(new ArticlesPage());
            Children.Add(new SemesterHarmonogramPage());
            Children.Add(new FinalsSchedulePage());
            Children.Add(new SoftwarePage());
            Children.Add(new OpeningHoursPage());
            Children.Add(new TemplatesPage());
        }
    }
}
