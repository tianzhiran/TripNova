using TripNova.ViewModels;
using TripNova.Services;

namespace TripNova.Views;

public partial class BudgetPage : ContentPage
{
    public BudgetPage()
    {
        InitializeComponent();

        var database = new DatabaseService();   // 创建数据库服务
        BindingContext = new BudgetViewModel(database);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is BudgetViewModel vm)
        {
            await vm.LoadTrips();   // 🔥先加载 Trip（会自动触发预算）
        }
    }
}