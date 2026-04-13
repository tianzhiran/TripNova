using SQLite;
using TripNova.Models;

namespace TripNova.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection _db;

    // ================= INIT =================

    private async Task Init()
    {
        if (_db != null)
            return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "tripnova.db");

        _db = new SQLiteAsyncConnection(dbPath);

        await _db.CreateTableAsync<User>();
        await _db.CreateTableAsync<Trip>();
        await _db.CreateTableAsync<BudgetItem>();
    }

    // ================= USER =================

    public async Task<int> AddUser(User user)
    {
        await Init();
        return await _db.InsertAsync(user);
    }

    public async Task<User> GetUser(string username, string password)
    {
        await Init();
        return await _db.Table<User>()
            .Where(u => u.Username == username && u.Password == password)
            .FirstOrDefaultAsync();
    }

    // ================= TRIP =================

    public async Task<int> AddTrip(Trip trip)
    {
        await Init();
        return await _db.InsertAsync(trip);
    }

    public async Task<List<Trip>> GetTrips(int userId)
    {
        await Init();
        return await _db.Table<Trip>()
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<Trip> GetTripById(int id)
    {
        await Init();
        return await _db.Table<Trip>()
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> UpdateTrip(Trip trip)
    {
        await Init();
        return await _db.UpdateAsync(trip);
    }

    public async Task<int> DeleteTrip(Trip trip)
    {
        await Init();
        return await _db.DeleteAsync(trip);
    }

    // ================= BUDGET =================

    public async Task<int> AddBudgetItem(BudgetItem item)
    {
        await Init();
        return await _db.InsertAsync(item);
    }

    public async Task<List<BudgetItem>> GetBudgetItems(int tripId)
    {
        await Init();
        return await _db.Table<BudgetItem>()
            .Where(b => b.TripId == tripId)
            .ToListAsync();
    }

    public async Task<int> DeleteBudgetItem(BudgetItem item)
    {
        await Init();
        return await _db.DeleteAsync(item);
    }

    // ================= CALCULATION =================

    public async Task<double> GetTotalSpent(int tripId)
    {
        await Init();

        var items = await _db.Table<BudgetItem>()
            .Where(b => b.TripId == tripId)
            .ToListAsync();

        return items.Sum(i => i.Amount);
    }
}