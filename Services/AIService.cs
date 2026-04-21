using System.Text.RegularExpressions;

namespace TripNova.Services;

public class AIService
{
    public async Task<string> GetBudgetSuggestion(string input)
    {
        // 🤖 模拟 AI 思考时间
        await Task.Delay(1200);

        // 🔍 提取预算金额（例如 1000）
        var budgetMatch = Regex.Match(input, @"\d+");
        double budget = budgetMatch.Success ? double.Parse(budgetMatch.Value) : 1000;

        // 🔍 提取天数（例如 5 days）
        var daysMatch = Regex.Match(input.ToLower(), @"\d+\s*day");
        int days =   daysMatch.Success ? int.Parse(Regex.Match(daysMatch.Value, @"\d+").Value) : 3;

        // 💡 模拟 AI 分配逻辑
        double accommodation = budget * 0.5;
        double food = budget * 0.25;
        double activities = budget * 0.25;

        // 📊 每日预算
        double perDay = budget / days;

        // 🤖 返回“像 AI 的回答”
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