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
            await vm.LoadBudgetItems();   // 🔥加载数据库数据
        }
    }
}