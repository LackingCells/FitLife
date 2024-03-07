using FitLife.Logic.DB;

namespace FitLife.Pages;


public partial class weightTrackingPage : ContentPage
{
    WeightRepo weightRepo;

    public weightTrackingPage(WeightRepo weightRepo)
    {
        InitializeComponent();
        this.weightRepo = weightRepo;

        //currentWeight.Text = weightRepo.GetWeekWeight(DateOnly.FromDateTime(DateTime.Today)).ToString();
    }

    private void weekWeightBtn_Clicked(object sender, EventArgs e)
    {
        currentWeight.Text = weightRepo.GetWeekWeight(DateOnly.FromDateTime(DateTime.Today)).ToString();
    }

    private void monthWeightBtn_Clicked(object sender, EventArgs e)
    {
        weightRepo.Test();
    }
}