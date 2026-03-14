namespace TripNova.Models;

public class Trip
{
    public string Destination { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public double Budget { get; set; }

    public int Travellers { get; set; }
}