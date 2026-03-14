using System.Collections.ObjectModel;
using System.Windows.Input;
using TripNova.Models;

namespace TripNova.ViewModels;

public class SearchViewModel
{
    public ObservableCollection<Destination> Destinations { get; set; }

    public ICommand PlanTripCommand { get; }

    public SearchViewModel()
    {
        Destinations = new ObservableCollection<Destination>();

        Destinations.Add(new Destination
        {
            Name = "Tokyo",
            Country = "Japan",
            Description = "Modern city with culture and technology"
        });

        Destinations.Add(new Destination
        {
            Name = "Paris",
            Country = "France",
            Description = "Romantic capital with historical landmarks"
        });

        Destinations.Add(new Destination
        {
            Name = "Bali",
            Country = "Indonesia",
            Description = "Famous tropical island destination"
        });

        PlanTripCommand = new Command<Destination>(PlanTrip);
    }

    private async void PlanTrip(Destination destination)
    {
        if (destination == null) return;

        await Shell.Current.GoToAsync("//Trips");

        // Later we can auto-fill trip data here
    }
}