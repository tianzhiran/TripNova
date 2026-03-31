namespace TripNova.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    async void OnSearchTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Search");
    }

    async void OnBudgetTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Budget");
    }

    async void OnAITapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AI");
    }

    async void OnTripsTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Trips");
    }

    async void OnTransportTapped(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://www.rome2rio.com");
    }

    async void OnAccommodationTapped(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://www.booking.com");
    }

    async void OnFoodTapped(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://www.google.com/maps");
    }

    async void OnItineraryTapped(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://www.tripadvisor.com");
    }
}