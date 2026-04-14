using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TripNova.Models;
using TripNova.Services;

namespace TripNova.ViewModels;

[QueryProperty(nameof(TripId), "tripId")]
public class CreateTripViewModel : INotifyPropertyChanged
{

    private int tripId;
    public int TripId
    {
        get => tripId;
        set
        {
            if (tripId == value) return;

            tripId = value;
            OnPropertyChanged();

            _ = LoadTrip();
        }
    }
    private readonly DatabaseService _db;

    // ---------------- INPUT ----------------

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

    // ---------------- COMMAND ----------------

    public ICommand SaveTripCommand { get; }

    public CreateTripViewModel()
    {
        _db = new DatabaseService();
        SaveTripCommand = new Command(async () => await SaveTrip());

        
    }

    // ---------------- SAVE ----------------

    private async Task SaveTrip()
    {
        if (string.IsNullOrWhiteSpace(Destination))
            return;

        if (TripId == 0)
        {
            // 🟢 Create
            var newTrip = new Trip
            {
                Destination = Destination,
                StartDate = StartDate,
                EndDate = EndDate,
                Budget = Budget,
                Travellers = Travellers,
                UserId = 1
            };

            await _db.AddTrip(newTrip);
        }
        else
        {
            // 🔥 Update（Edit）
            var updatedTrip = new Trip
            {
                Id = TripId,
                Destination = Destination,
                StartDate = StartDate,
                EndDate = EndDate,
                Budget = Budget,
                Travellers = Travellers,
                UserId = 1
            };

            await _db.UpdateTrip(updatedTrip);
        }

        await Shell.Current.GoToAsync("..");
    }

    private async Task LoadTrip()
    {
        if (TripId == 0)
            return;

        var trip = await _db.GetTripById(TripId);

        if (trip != null)
        {
            Destination = trip.Destination;
            StartDate = trip.StartDate;
            EndDate = trip.EndDate;
            Budget = trip.Budget;
            Travellers = trip.Travellers;
        }
    }

    // ---------------- NOTIFY ----------------

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}