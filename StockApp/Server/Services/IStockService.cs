using StockApp.Server.Data.DTOs.Stock;
using StockApp.Shared;


namespace StockApp.Server.Services
{
    public interface IStockService
    {
        public Task<StockDTO> GetStockFromAPI(string symbol);
        public Task<StockDTO> GetStockFromDatabase(string symbol);
        public Task<bool> AreAnyStocksInDb();
        public Task<IEnumerable<StockSearchDTO>> GetStocks();
        public Task<bool> IsStockInDb(string symbol);
        public Task SaveChangesAsync();

    }
}
