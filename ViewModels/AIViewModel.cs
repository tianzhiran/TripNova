using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TripNova.Services;

namespace TripNova.ViewModels;

public class AIViewModel : INotifyPropertyChanged
{
    private readonly AIService _aiService;

    public AIViewModel()
    {
        _aiService = new AIService();
        GetSuggestionCommand = new Command(async () => await GetSuggestion());
    }

    // 🔹 用户输入
    private string userInput;
    public string UserInput
    {
        get => userInput;
        set { userInput = value; OnPropertyChanged(); }
    }

    // 🔹 AI返回结果
    private string aiResult;
    public string AIResult
    {
        get => aiResult;
        set { aiResult = value; OnPropertyChanged(); }
    }

    // 🔹 按钮命令
    public ICommand GetSuggestionCommand { get; }

    // 🔥 核心逻辑
    private async Task GetSuggestion()
    {
        if (string.IsNullOrWhiteSpace(UserInput))
            return;

        AIResult = "Thinking... 🤖";

        var result = await _aiService.GetBudgetSuggestion(UserInput);

        AIResult = result;
    }

    // ---------------- NOTIFY ----------------

    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string name = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}