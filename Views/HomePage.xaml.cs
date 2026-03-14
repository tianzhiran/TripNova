namespace TripNova.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void OnSearchTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Search");
    }

    private async void OnBudgetTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Budget");
    }

    private async void OnAITapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AI");
    }

    private async void OnTripsTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Trips");
    }

    private async void OnTransportTapped(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://www.rome2rio.com");
    }

    private async void OnAccommodationTapped(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://www.booking.com");
    }

    private async void OnFoodTapped(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://www.google.com/maps");
    }

    private async void OnItineraryTapped(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://www.tripadvisor.com");
    }
}