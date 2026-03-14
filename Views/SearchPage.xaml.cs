using TripNova.ViewModels;

namespace TripNova.Views;

public partial class SearchPage : ContentPage
{
    public SearchPage()
    {
        InitializeComponent();
        BindingContext = new SearchViewModel();
    }
}