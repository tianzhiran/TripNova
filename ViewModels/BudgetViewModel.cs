using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TripNova.Models;

namespace TripNova.ViewModels;

public class BudgetViewModel : INotifyPropertyChanged
{
    public ObservableCollection<BudgetItem> Items { get; } = new();

    private string category;
    public List<string> Categories { get; } = new List<string>
    {
        "Transportation",
        "Accommodation",
        "Food & Dining",
        "Activities",
        "Shopping",
        "Other"
    };
    public string Category
    {
        get => category;
        set { category = value; OnPropertyChanged(); }
    }

    private string description;
    public string Description
    {
        get => description;
        set { description = value; OnPropertyChanged(); }
    }

    private double amount;
    public double Amount
    {
        get => amount;
        set { amount = value; OnPropertyChanged(); }
    }

    public ICommand AddItemCommand { get; }
    public ICommand DeleteItemCommand { get; }

    public BudgetViewModel()
    {
        AddItemCommand = new Command(AddItem);
        DeleteItemCommand = new Command<BudgetItem>(DeleteItem);

        // sample data
        Items.Add(new BudgetItem { Category = "Transportation", Description = "Flight ticket", Amount = 850 });
        Items.Add(new BudgetItem { Category = "Accommodation", Description = "Hotel stay", Amount = 600 });

        Items.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalBudget));
    }

    public double TotalBudget => Items.Sum(i => i.Amount);

    private void AddItem()
    {
        if (string.IsNullOrWhiteSpace(Category) || Amount <= 0)
            return;

        Items.Add(new BudgetItem
        {
            Category = Category,
            Description = Description,
            Amount = Amount
        });

        Category = "";
        Description = "";
        Amount = 0;

        OnPropertyChanged(nameof(Category));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(Amount));
    }

    private void DeleteItem(BudgetItem item)
    {
        if (item != null)
        {
            Items.Remove(item);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string name = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}