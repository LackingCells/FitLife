using FitLife.Logic.DB;
using FitLife.Logic.ViewModels;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace FitLife.Pages;


public partial class weightTrackingPage : ContentPage
{
    private readonly DBService _dbService;
    private List<Weight> weightList = new List<Weight>();
    private List<Macro> macroList = new List<Macro>();
    private VMWeightChart chart;

    public weightTrackingPage(DBService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
        Task.Run(async () => weightList = await _dbService.GetWeekWeight(DateTime.Today));
        Task.Run(async () => macroList = await _dbService.GetWeekMacro(DateTime.Today));
        currentWeight.Text = getWeightAverageOf(weightList).ToString() + " kg";
        chart = BindingContext as VMWeightChart;
        chart.UpdateWeightChart(weightList);
        chart.UpdateMacroChart(macroList);
        //uppdatera charten, macro och weight

    }

    //Har kvar för att komma ihåg hur create funkar
    //private async void monthWeightBtn_Clicked(object sender, EventArgs e)
    //{
    //    await _dbService.CreateWeight(new Weight
    //    {
    //        DailyWeight = 87,
    //        Date = DateTime.Today
    //    });
    //    await _dbService.CreateWeight(new Weight
    //    {
    //        DailyWeight = 89,
    //        Date = DateTime.Today.AddDays(2)
    //    });
    //    await _dbService.CreateWeight(new Weight
    //    {
    //        DailyWeight = 91,
    //        Date = DateTime.Today.AddDays(4)
    //    });
    //}

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

    private async void AllTimeButton_Clicked(object sender, EventArgs e)
    {
        weightList = await _dbService.GetAllTimeWeight();
        macroList = await _dbService.GetAllTimeMacro();
        chart.UpdateWeightChart(weightList);
        chart.UpdateMacroChart(macroList);
    }
}