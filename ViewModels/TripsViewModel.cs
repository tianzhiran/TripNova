using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TripNova.Models;

namespace TripNova.ViewModels;

public class TripsViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Trip> Trips { get; } = new();

    private string destination;
    public string Destination
    {
        get => destination;
        set { destination = value; OnPropertyChanged(); }
    }

    private DateTime startDate = DateTime.Today;
    public DateTime StartDate
    {
        get => startDate;
        set { startDate = value; OnPropertyChanged(); }
    }

    private DateTime endDate = DateTime.Today.AddDays(5);
    public DateTime EndDate
    {
        get => endDate;
        set { endDate = value; OnPropertyChanged(); }
    }

    private double budget;
    public double Budget
    {
        get => budget;
        set { budget = value; OnPropertyChanged(); }
    }

    private int travellers = 1;
    public int Travellers
    {
        get => travellers;
        set { travellers = value; OnPropertyChanged(); }
    }

    public ICommand AddTripCommand { get; }

    public TripsViewModel()
    {
        AddTripCommand = new Command(AddTrip);
    }

    private void AddTrip()
    {
        if (string.IsNullOrWhiteSpace(Destination))
            return;

        Trips.Add(new Trip
        {
            Destination = Destination,
            StartDate = StartDate,
            EndDate = EndDate,
            Budget = Budget,
            Travellers = Travellers
        });

        Destination = "";
        Budget = 0;
        Travellers = 1;

        OnPropertyChanged(nameof(Destination));
        OnPropertyChanged(nameof(Budget));
        OnPropertyChanged(nameof(Travellers));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string name = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}