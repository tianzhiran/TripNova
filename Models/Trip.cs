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
}