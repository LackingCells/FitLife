namespace FitLife
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXxfeHVUQmFYWUxwXEA=");
            InitializeComponent();

            MainPage = new AppShell();

        }
    }
}
