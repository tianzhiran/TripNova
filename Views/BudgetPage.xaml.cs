using TripNova.ViewModels;

namespace TripNova.Views;

public partial class BudgetPage : ContentPage
{
    public BudgetPage()
    {
        InitializeComponent();
        BindingContext = new BudgetViewModel();
    }
}   