using FitLife.Logic.DB;

namespace FitLife.Pages;

public partial class settingPage : ContentPage
{
    private readonly DBService _dbService;
    public settingPage(DBService dbService)
	{
		InitializeComponent();
        _dbService = dbService;
    }

    private async void SaveWeight_Clicked(object sender, EventArgs e)
    {
        await _dbService.CreateWeight(new Weight
        {
            DailyWeight = float.Parse(weightKg.Text),
            Date = weightDate.Date
        });
        weightDate.Date = DateTime.Now;
        weightKg.Text = string.Empty;
    }

    private async void SaveKcalProtein_Clicked(object sender, EventArgs e)
    {
        await _dbService.CreateMacro(new Macro
        {
            Kcal = int.Parse(Kcal.Text),
            Protein = int.Parse(Protein.Text),
            Date = kcalProteinDate.Date
        });
        Kcal.Text = string.Empty;
        Protein.Text = string.Empty;
        kcalProteinDate.Date = DateTime.Today;
    }
}