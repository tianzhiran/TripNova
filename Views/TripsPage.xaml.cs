using TripNova.ViewModels;

namespace TripNova.Views;

public partial class TripsPage : ContentPage
{
    public TripsPage()
    {
        InitializeComponent();
        BindingContext = new TripsViewModel();
    }
}