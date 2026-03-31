using SQLite;
using TripNova.Models;

namespace TripNova.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection _db;

    public async Task Init()
    {
        if (_db != null)
            return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "tripnova.db");

        _db = new SQLiteAsyncConnection(dbPath);

        await _db.CreateTableAsync<User>();
        await _db.CreateTableAsync<Trip>();
        await _db.CreateTableAsync<BudgetItem>();
    }

    // ---------------- USER ----------------

    public Task<int> AddUser(User user)
    {
        return _db.InsertAsync(user);
    }

    public Task<User> GetUser(string username, string password)
    {
        return _db.Table<User>()
            .Where(u => u.Username == username && u.Password == password)
            .FirstOrDefaultAsync();
    }

    // ---------------- TRIP ----------------

    public Task<int> AddTrip(Trip trip)
    {
        return _db.InsertAsync(trip);
    }

    public Task<List<Trip>> GetTrips(int userId)
    {
        return _db.Table<Trip>()
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public Task<int> DeleteTrip(Trip trip)
    {
        return _db.DeleteAsync(trip);
    }

    public Task<int> UpdateTrip(Trip trip)
    {
        return _db.UpdateAsync(trip);
    }

    public Task<Trip> GetTripById(int id)
    {
        return _db.Table<Trip>()
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();
    }

    // ---------------- BUDGET ----------------

    public Task<int> AddBudgetItem(BudgetItem item)
    {
        return _db.InsertAsync(item);
    }

    public Task<List<BudgetItem>> GetBudgetItems(int tripId)
    {
        return _db.Table<BudgetItem>()
            .Where(b => b.TripId == tripId)
            .ToListAsync();
    }
}