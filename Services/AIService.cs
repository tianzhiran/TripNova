using System.Text.RegularExpressions;

namespace TripNova.Services;

public class AIService
{
    public async Task<string> GetBudgetSuggestion(string input)
    {
        // 🤖 Simulated AI thinking time
        await Task.Delay(1200);

        // 🔍 Extract the budget amount (e.g., 1000).
        var budgetMatch = Regex.Match(input, @"\d+");
        double budget = budgetMatch.Success ? double.Parse(budgetMatch.Value) : 1000;

        // 🔍 Number of days to extract (e.g., 5 days)
        var daysMatch = Regex.Match(input.ToLower(), @"\d+\s*day");
        int days =   daysMatch.Success ? int.Parse(Regex.Match(daysMatch.Value, @"\d+").Value) : 3;

        // 💡 simulate a simple budget allocation strategy
        double accommodation = budget * 0.5;
        double food = budget * 0.25;
        double activities = budget * 0.25;

        // 📊 Daily budget calculation
        double perDay = budget / days;

        // 🤖 return a formatted suggestion
        return $"🤖 AI Budget Plan\n\n" +
               $"Total Budget: ${budget}\n" +
               $"Trip Duration: {days} days\n\n" +
               $"Suggested Allocation:\n" +
               $"- 🏨 Accommodation: ${accommodation:F0}\n" +
               $"- 🍜 Food: ${food:F0}\n" +
               $"- 🎯 Activities: ${activities:F0}\n\n" +
               $"📅 Daily Budget: ${perDay:F0} per day\n\n" +
               $"💡 Tip: Keep 10% buffer for unexpected expenses!";
    }
}