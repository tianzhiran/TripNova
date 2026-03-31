using TripNova.Views; 

namespace TripNova;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // ✅ Register route here
        Routing.RegisterRoute(nameof(CreateTripPage), typeof(CreateTripPage));
    }
}