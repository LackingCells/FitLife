namespace FitLife
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            switch (DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    ConstantsDB.daysToMonday = -0;
                    ConstantsDB.daysToSunday = 6;
                    break;
                case DayOfWeek.Tuesday:
                    ConstantsDB.daysToMonday = -1;
                    ConstantsDB.daysToSunday = 5;
                    break;
                case DayOfWeek.Wednesday:
                    ConstantsDB.daysToMonday = -2;
                    ConstantsDB.daysToSunday = 4;
                    break;
                case DayOfWeek.Thursday:
                    ConstantsDB.daysToMonday = -3;
                    ConstantsDB.daysToSunday = 3;
                    break;
                case DayOfWeek.Friday:
                    ConstantsDB.daysToMonday = -4;
                    ConstantsDB.daysToSunday = 2;
                    break;
                case DayOfWeek.Saturday:
                    ConstantsDB.daysToMonday = -5;
                    ConstantsDB.daysToSunday = 1;
                    break;
                case DayOfWeek.Sunday:
                    ConstantsDB.daysToMonday = -6;
                    ConstantsDB.daysToSunday = 0;
                    break;
            }

        }
    }
}
