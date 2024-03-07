using FitLife.Logic.DB;
using System.Diagnostics;

namespace FitLife.Pages;


public partial class weightTrackingPage : ContentPage
{
    private readonly DBService _dbService;
    List<Weight> weightList = new List<Weight>();

    public weightTrackingPage(DBService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
        
    }

    private void weekWeightBtn_Clicked(object sender, EventArgs e)
    {
        
        Task.Run(async () => weightList = await _dbService.GetWeekWeight());
        currentWeight.Text = getWeightAverageOf(weightList).ToString();
    }

    private async void monthWeightBtn_Clicked(object sender, EventArgs e)
    {
        await _dbService.Create(new Weight
        {
            DailyWeight = 87
        });
        await _dbService.Create(new Weight
        {
            DailyWeight = 83
        });
        await _dbService.Create(new Weight
        {
            DailyWeight = 84
        });

        weightList = await _dbService.GetWeekWeight();
        currentWeight.Text = getWeightAverageOf(weightList).ToString();
    }

    private float getWeightAverageOf(List<Weight> weeklyWeight)
    {
        float sum = 0;
        foreach (Weight weight in weeklyWeight)
        {
            sum += weight.DailyWeight;
        }
        sum = sum / weeklyWeight.Count;

        return sum;
    }
}