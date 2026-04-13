using TripNova.ViewModels;

namespace TripNova.Views;

public partial class TripsPage : ContentPage
{
    private TripsViewModel _viewModel;

    public TripsPage()
    {
        InitializeComponent();

        _viewModel = new TripsViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is TripsViewModel vm)
        {
            await vm.LoadTrips();
        }
    }
}