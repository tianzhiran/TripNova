using TripNova.ViewModels;

namespace TripNova.Views;

public partial class CreateTripPage : ContentPage
{
    public CreateTripPage()
    {
        InitializeComponent();
        BindingContext = new CreateTripViewModel();
    }
}