using SQLite;

namespace TripNova.Models;

public class BudgetItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int TripId { get; set; }   // 🔥 关键：关联 Trip

    public string Category { get; set; }

    public string Description { get; set; }

    public double Amount { get; set; }
}