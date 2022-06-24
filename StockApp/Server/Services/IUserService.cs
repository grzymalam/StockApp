

using StockApp.Shared;

namespace StockApp.Server.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<StockDTO>> GetClientStocks(string id);
        public Task<bool> DoesStockExist(string symbol);
        public Task<bool> DoesUserWatchStock(string username, string symbol);
        public Task AddStockToWatchlistForUser(string username, string symbol);
        public Task RemoveStockFromWatchlist(string username, string symbol);
        public Task SaveChangesAsync();
    }
}
