using Microsoft.EntityFrameworkCore;
using StockApp.Server.Data;
using StockApp.Server.Data.DTOs.Stock;
using StockApp.Server.Models;
using StockApp.Shared;
namespace StockApp.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        
        public UserService(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task AddStockToWatchlistForUser(string username, string symbol)
        {
            await _context.AddAsync(new UserStock
            {
                ApplicationUserId = username,
                Symbol = symbol
            });
        
        }

        public async Task<bool> DoesStockExist(string symbol)
        {
            return await _context.CachedStocks.AnyAsync(x => x.Symbol == symbol);
        }

        public async Task<bool> DoesUserWatchStock(string username, string symbol)
        {
            return await _context.UserStocks.AnyAsync(x => x.ApplicationUserId == username && x.Symbol == symbol);
        }

        public async Task<IEnumerable<StockDTO>> GetClientStocks(string username)
        {
            var symbols = await _context.UserStocks
                .Where(x => x.ApplicationUserId == username)
                .Select(x => x.Symbol)
                .ToListAsync();
            List<CachedStock> stocks = await _context.CachedStocks.Where(x => symbols.Contains(x.Symbol)).ToListAsync();
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var returned = stocks.Select(e => new StockDTO
            {
                Symbol = e.Symbol,
                Name = e.Name,
                Country = e.Country,
                LogoURL = e.LogoURL,
                Date = dt.AddMilliseconds(e.UpdateTime),
                Tags = e.Tags,
                Volume = e.Volume,
                High = e.High,
                Low = e.Low,
                Open = e.Open,
                Close = e.Close
            });
            return returned;
        }
        
        public async Task RemoveStockFromWatchlist(string username, string symbol)
        {
            var toDelete = await _context.UserStocks.Where(x => x.ApplicationUserId == username && x.Symbol == symbol).ToListAsync();
            _context.UserStocks.RemoveRange(toDelete);
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
