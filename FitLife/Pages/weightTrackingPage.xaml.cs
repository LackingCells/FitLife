using FitLife.Logic.DB;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

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

    private async void weekWeightBtn_Clicked(object sender, EventArgs e)
    {
        weightList = await _dbService.GetWeekWeight(DateTime.Today);
        currentWeight.Text = getWeightAverageOf(weightList).ToString();
        addedWeight.Text = getDateAverageOf(weightList).ToString();
        //addedWeight.Text = DateTime.Today.ToString();
    }

    private async void monthWeightBtn_Clicked(object sender, EventArgs e)
    {
        await _dbService.CreateWeight(new Weight
        {
            DailyWeight = 87,
            Date = DateTime.Today
        });
        await _dbService.CreateWeight(new Weight
        {
            DailyWeight = 150,
            Date = DateTime.Today.AddDays(10)
        });
        await _dbService.CreateWeight(new Weight
        {
            DailyWeight = 84,
            Date = DateTime.Today.AddDays(-1)
        });
    }

    private float getWeightAverageOf(List<Weight> weeklyWeight)
    {
        float sum = 0;
        foreach (Weight weight in weeklyWeight)
        {
            sum += weight.DailyWeight;
        }
        float avg = sum / weeklyWeight.Count;

        return avg;
    }

    private DateTime getDateAverageOf(List<Weight> weeklyWeight)
    {
        List<DateTime> sum = new List<DateTime>(weeklyWeight.Count);
        double count = weeklyWeight.Count;
        double temp = 0;

        foreach (Weight weight in weeklyWeight)
        {
            sum.Add(weight.Date);
        }

        foreach (Weight time in weeklyWeight)
        {
            temp += time.Date.Ticks / count;
        }
        return new DateTime((long)temp);
    }
}