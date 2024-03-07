using FitLife.Logic.DB;

namespace FitLife.Pages;


public partial class weightTrackingPage : ContentPage
{
    WeightRepo weightRepo;

    public weightTrackingPage(WeightRepo weightRepo)
	{
		InitializeComponent();
        this.weightRepo = weightRepo;
	}

    private void weekWeightBtn_Clicked(object sender, EventArgs e)
    {

    }

    private void monthWeightBtn_Clicked(object sender, EventArgs e)
    {

    }
}