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
}