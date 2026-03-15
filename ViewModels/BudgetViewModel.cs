using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TripNova.Models;

namespace TripNova.ViewModels;

public class BudgetViewModel : INotifyPropertyChanged
{
    public ObservableCollection<BudgetItem> Items { get; } = new();

    public ICommand ShowAddDialogCommand { get; }
    public ICommand DeleteItemCommand { get; }

    public BudgetViewModel()
    {
        ShowAddDialogCommand = new Command(ShowAddDialog);
        DeleteItemCommand = new Command<BudgetItem>(DeleteItem);

        // sample data
        Items.Add(new BudgetItem { Category = "Transportation", Description = "Flight Ticket", Amount = 850 });
        Items.Add(new BudgetItem { Category = "Accommodation", Description = "Hotel", Amount = 600 });

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

        Items.Add(new BudgetItem
        {
            Category = category,
            Description = description,
            Amount = amount
        });
    }

    private void DeleteItem(BudgetItem item)
    {
        if (item != null)
            Items.Remove(item);
    }

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

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}