using TripNova.ViewModels;

namespace TripNova.Views;

public partial class AIPage : ContentPage
{
    public AIPage()
    {
        InitializeComponent();
        BindingContext = new AIViewModel();
    }
}