using SQLite;

namespace TripNova.Models;

public class Trip
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int UserId { get; set; }   // 🔥 NEW

    public string Destination { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public double Budget { get; set; }

    public int Travellers { get; set; }
    
    [Ignore]
    public string Status
    {
        get
        {
            var today = DateTime.Today;

            if (StartDate > today)
                return "Upcoming";

            if (StartDate <= today && EndDate >= today)
                return "Happening";

            return "Past";
        }
    }

    [Ignore]
    public string CountdownText
    {
        get
        {
            var today = DateTime.Today;

            if (StartDate > today)
            {
                int days = (StartDate - today).Days;
                return $"Starts in {days} days";
            }

            if (StartDate <= today && EndDate >= today)
            {
                int daysLeft = (EndDate - today).Days;
                return $"Ongoing • {daysLeft} days left";
            }

            return "Trip completed";
        }
    }

    [Ignore]
    public bool IsOverBudget { get; set; }

    [Ignore]
    public string BudgetStatusText
    {
        get => IsOverBudget ? "⚠️ Over Budget" : "";
    }

    [Ignore]
    public double TotalSpent { get; set; }

    [Ignore]
    public double RemainingBudget => Budget - TotalSpent;

    [Ignore]
    public double Progress => Budget == 0 ? 0 : TotalSpent / Budget;

    [Ignore]
    public string BudgetSummary
    {
        get
        {
            if (TotalSpent > Budget)
                return $"Over by ${TotalSpent - Budget} ⚠️";

            return $"Remaining: ${RemainingBudget}";
        }
    }

    [Ignore]
    public bool IsNearLimit => Progress >= 0.8 && Progress <= 1.0;

    [Ignore]
    public string WarningText
    {
        get
        {
            if (Progress > 1.0)
                return "⚠️ Over Budget";

            if (Progress >= 0.8)
                return "⚠️ Near Budget Limit";

            return "";
        }
    }

    [Ignore]
    public Color ProgressColor
    {
        get
        {
            if (Progress > 1.0)
                return Colors.Red;

            if (Progress >= 0.8)
                return Colors.Orange;

            return Colors.Green;
        }
    }
}