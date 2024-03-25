using FitLife.Logic.DB;
using FitLife.Logic.ViewModels;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        InitializeAsyncs();
    }

    private async void InitializeAsyncs()
    {
        weightList = await getWeekWeight(DateTime.Today);
        weightChange.Text = "Weight change: " + (getWeightAverageOf(await getWeekWeight(DateTime.Today)) - getWeightAverageOf(await getWeekWeight(DateTime.Today.AddDays(-7)))) + " kg";

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            currentWeight.Text = getWeightAverageOf(weightList) + " kg";
            chart = BindingContext as VMWeightChart;
        });
    }

    

    //METHODS
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
    private void UpdateLists()
    {
        chart.UpdateWeightChart(weightList);
        chart.UpdateMacroChart(macroList);
    }

    private async Task<List<Weight>> getWeekWeight(DateTime date)
    {
        DateTime monday = getThisMonday(date);
        List<Weight> output = new List<Weight>();
        Weight weight = new Weight();

        for (int i = 0; i < 7; i++)
        {
            weight = await _dbService.getWeight(monday.AddDays(i));
            if (weight != null)
            {
                output.Add(weight);
            }
        }
        return output;
    }
    private DateTime getThisMonday(DateTime date)
    {
        switch (date.DayOfWeek)
        {
            case DayOfWeek.Monday:
                return date;
            case DayOfWeek.Tuesday:
                return date.AddDays(-1);
            case DayOfWeek.Wednesday:
                return date.AddDays(-2);
            case DayOfWeek.Thursday:
                return date.AddDays(-3);
            case DayOfWeek.Friday:
                return date.AddDays(-4);
            case DayOfWeek.Saturday:
                return date.AddDays(-5);
            case DayOfWeek.Sunday:
                return date.AddDays(-6);
            default:
                break;
        }
        return date;
    }



    //BUTTONS
    private async void AllTimeButton_Clicked(object sender, EventArgs e)
    {
        weightList = await _dbService.GetAllTimeWeight();
        macroList = await _dbService.GetAllTimeMacro();
        UpdateLists();
        currentWeight.Text = getWeightAverageOf(weightList) + " kg";
    }

    private async void WeightButton_Clicked(object sender, EventArgs e)
    {
        if (weightKg.Text != null)
        {
            await _dbService.CreateWeight(new Weight
            {
                DailyWeight = float.Parse(weightKg.Text),
                Date = DateTime.Today
            }, this);
            weightKg.Text = "weight entered";
        }

    }

    private async void KcalProteinButton_Clicked(object sender, EventArgs e)
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
                Date = DateTime.Today
            }, this);
            Kcal.Text = null;
            Protein.Text = null;
        }
    }

    private async void NewWeightButton_Clicked(object sender, EventArgs e)
    {
    }
}