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
        Task.Run(async () => weightList = await _dbService.GetWeekWeight(DateTime.Today)); //problem med detta, funkar inte
        Task.Run(async () => macroList = await _dbService.GetWeekMacro(DateTime.Today)); //samma
        currentWeight.Text = getWeightAverageOf(weightList) + " kg";
        Debug.WriteLine(getWeightAverageOf(weightList));
        chart = BindingContext as VMWeightChart;
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
    public void UpdateLists()
    {
        chart.UpdateWeightChart(weightList);
        chart.UpdateMacroChart(macroList);
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
}