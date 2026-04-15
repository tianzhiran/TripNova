using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TripNova.Models;
using TripNova.Services;

namespace TripNova.ViewModels;

[QueryProperty(nameof(TripId), "tripId")]
public class BudgetViewModel : INotifyPropertyChanged
{
    private readonly DatabaseService _database;

    public int TripId { get; set; }

    public ObservableCollection<BudgetItem> Items { get; } = new();
    public ObservableCollection<Trip> Trips { get; } = new();

    public ICommand ShowAddDialogCommand { get; }
    public ICommand DeleteItemCommand { get; }

    public BudgetViewModel(DatabaseService database)
    {
        _database = database;

        ShowAddDialogCommand = new Command(ShowAddDialog);
        DeleteItemCommand = new Command<BudgetItem>(DeleteItem);

        Items.CollectionChanged += (s, e) =>
        {
            OnPropertyChanged(nameof(TotalBudget));
            OnPropertyChanged(nameof(TransportationTotal));
            OnPropertyChanged(nameof(AccommodationTotal));
            OnPropertyChanged(nameof(FoodTotal));
            OnPropertyChanged(nameof(ActivitiesTotal));
            OnPropertyChanged(nameof(ShoppingTotal));
            OnPropertyChanged(nameof(OtherTotal));
        };
    }

    // ================= LOAD FROM DATABASE =================

    public async Task LoadBudgetItems()
    {
        // ❗如果没有传 TripId（从 Budget tab 进入）
        if (TripId == 0)
        {
            var trips = await _database.GetTrips(1); // temporary userId

            if (trips.Count > 0)
            {
                TripId = trips[0].Id; // 默认第一个 Trip
            }
            else
            {
                // 没有任何 trip，直接清空
                Items.Clear();
                return;
            }
        }

        var items = await _database.GetBudgetItems(TripId);

        Items.Clear();

        foreach (var item in items)
            Items.Add(item);
    }

    // ================= ADD =================

    private async void ShowAddDialog()
    {
        string category = await Application.Current.MainPage.DisplayActionSheet(
            "Select Category",
            "Cancel",
            null,
            "Transportation",
            "Accommodation",
            "Food & Dining",
            "Activities",
            "Shopping",
            "Other");

        if (category == "Cancel" || category == null)
            return;

        string description = await Application.Current.MainPage.DisplayPromptAsync(
            "Description",
            "Enter expense description");

        string amountText = await Application.Current.MainPage.DisplayPromptAsync(
            "Amount",
            "Enter amount",
            keyboard: Keyboard.Numeric);

        if (!double.TryParse(amountText, out double amount) || amount <= 0)
            return;

        var newItem = new BudgetItem
        {
            TripId = TripId,   // 🔥关键连接！
            Category = category,
            Description = description,
            Amount = amount
        };

        await _database.AddBudgetItem(newItem);

        Items.Add(newItem);
        await CheckBudgetExceeded();
    }


    // ================= selectedTrip =================
    private Trip _selectedTrip;
    public Trip SelectedTrip
    {
        get => _selectedTrip;
        set
        {
            _selectedTrip = value;
            OnPropertyChanged();

            if (value != null)
            {
                TripId = value.Id;
                _ = LoadBudgetItems(); // 自动刷新🔥
            }
        }
    }

    // ================= LoadTrips =================
    public async Task LoadTrips()
    {
        var trips = await _database.GetTrips(1);

        Trips.Clear();

        foreach (var trip in trips)
            Trips.Add(trip);

        // 🔥 关键逻辑（兼容两种入口）
        if (Trips.Count > 0)
        {
            if (TripId != 0)
            {
                SelectedTrip = Trips.FirstOrDefault(t => t.Id == TripId);
            }
            else
            {
                SelectedTrip = Trips[0];
            }
        }
    }
    // ================= Budget Alert =================

    private async Task CheckBudgetExceeded()
    {
        if (SelectedTrip == null)
            return;

        double total = Items.Sum(i => i.Amount);

        if (total > SelectedTrip.Budget)
        {
            var request = new NotificationRequest
            {
                NotificationId = DateTime.Now.Millisecond + 2000,
                Title = "⚠️ Budget Alert",
                Description = "You have exceeded your trip budget!"
            };

            await LocalNotificationCenter.Current.Show(request);
        }
    }

    // ================= DELETE =================

    private async void DeleteItem(BudgetItem item)
    {
        if (item == null)
            return;

        await _database.DeleteBudgetItem(item);

        Items.Remove(item);
        await CheckBudgetExceeded();
    }

    // ================= CALCULATIONS =================

    public double TotalBudget => Items.Sum(i => i.Amount);

    public double TransportationTotal =>
        Items.Where(i => i.Category == "Transportation").Sum(i => i.Amount);

    public double AccommodationTotal =>
        Items.Where(i => i.Category == "Accommodation").Sum(i => i.Amount);

    public double FoodTotal =>
        Items.Where(i => i.Category == "Food & Dining").Sum(i => i.Amount);

    public double ActivitiesTotal =>
        Items.Where(i => i.Category == "Activities").Sum(i => i.Amount);

    public double ShoppingTotal =>
        Items.Where(i => i.Category == "Shopping").Sum(i => i.Amount);

    public double OtherTotal =>
        Items.Where(i => i.Category == "Other").Sum(i => i.Amount);

    // ================= INotify =================

    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string name = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}