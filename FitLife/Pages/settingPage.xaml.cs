using FitLife.Logic.DB;
using System.Diagnostics;

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
        if (weightKg.Text != null)
        {
            await _dbService.CreateWeight(new Weight
            {
                DailyWeight = float.Parse(weightKg.Text),
                Date = weightDate.Date
            });
            weightDate.Date = DateTime.Now;
            weightKg.Text = null;
        }
    }

    private async void SaveKcalProtein_Clicked(object sender, EventArgs e)
    {
        if (Kcal.Text != null || Protein.Text != null)
        {
            if (Kcal.Text == null) 
            {
                Kcal.Text = "0";
            }
            if (Protein.Text == null)
            {
                Protein.Text = "0";
            }

            await _dbService.CreateMacro(new Macro
            {
                Kcal = int.Parse(Kcal.Text),
                Protein = int.Parse(Protein.Text),
                Date = kcalProteinDate.Date
            });
            Kcal.Text = null;
            Protein.Text = null;
            kcalProteinDate.Date = DateTime.Today;
        }
    }
}