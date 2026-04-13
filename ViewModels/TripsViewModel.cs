using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TripNova.Models;
using TripNova.Services;

namespace TripNova.ViewModels;

public class TripsViewModel : INotifyPropertyChanged
{
    private readonly DatabaseService _db;

    // ---------------- COLLECTION ----------------

    public ObservableCollection<Trip> Trips { get; } = new();

    private List<Trip> allTrips = new();

    // ---------------- FILTER ----------------

    private string selectedFilter = "All";
    public string SelectedFilter
    {
        get => selectedFilter;
        set
        {
            selectedFilter = value;
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    // ---------------- COMMANDS ----------------

    public ICommand GoToCreateTripCommand { get; }
    public ICommand DeleteTripCommand { get; }
    public ICommand FilterCommand { get; }
    public ICommand OpenBudgetCommand { get; }

    // ---------------- CONSTRUCTOR ----------------

    public TripsViewModel()
    {
        _db = new DatabaseService();

        // Navigation
        GoToCreateTripCommand = new Command(async () => await GoToCreateTrip());

        // Delete
        DeleteTripCommand = new Command<Trip>(async (trip) => await DeleteTrip(trip));

        // Filter
        FilterCommand = new Command<string>((filter) =>
        {
            SelectedFilter = filter;
        });
        OpenBudgetCommand = new Command<Trip>(async (trip) => await OpenBudget(trip));

        Init();
    }

    // ---------------- INIT ----------------

    private async void Init()
    {
        await LoadTrips();
    }

    // ---------------- LOAD ----------------

    public async Task LoadTrips()
    {
        allTrips = await _db.GetTrips(1); // temporary UserId

        ApplyFilter();
    }

    public async Task OpenBudget(Trip trip)
    {
        await Shell.Current.GoToAsync($"BudgetPage?tripId={trip.Id}");
    }


    // ---------------- FILTER LOGIC ----------------

    private void ApplyFilter()
    {
        Trips.Clear();

        IEnumerable<Trip> filtered = allTrips;

        if (SelectedFilter == "Upcoming")
        {
            filtered = allTrips.Where(t => t.StartDate > DateTime.Today);
        }
        else if (SelectedFilter == "Past")
        {
            filtered = allTrips.Where(t => t.EndDate < DateTime.Today);
        }
        else if (SelectedFilter == "Planning")
        {
            filtered = allTrips.Where(t =>
                t.StartDate >= DateTime.Today &&
                t.EndDate >= DateTime.Today);
        }

        foreach (var trip in filtered)
        {
            Trips.Add(trip);
        }
    }

    // ---------------- DELETE ----------------

    private async Task DeleteTrip(Trip trip)
    {
        if (trip == null)
            return;

        await _db.DeleteTrip(trip);

        await LoadTrips();
    }

    // ---------------- NAVIGATION ----------------

    private async Task GoToCreateTrip()
    {
        await Shell.Current.GoToAsync(nameof(TripNova.Views.CreateTripPage));
    }

    // ---------------- NOTIFY ----------------

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}