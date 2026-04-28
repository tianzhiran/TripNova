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

    // 🔹 user input
    private string userInput;
    public string UserInput
    {
        get => userInput;
        set { userInput = value; OnPropertyChanged(); }
    }

    // 🔹 AI result
    private string aiResult;
    public string AIResult
    {
        get => aiResult;
        set { aiResult = value; OnPropertyChanged(); }
    }

    // 🔹 command to trigger AI suggestion
    public ICommand GetSuggestionCommand { get; }

    // 🔥 logic to get AI suggestion based on user input
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