using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockApp.Server.Data;
using StockApp.Server.Data.DTOs.Stock;
using StockApp.Server.Data.Models;
using StockApp.Server.Models;
using StockApp.Shared;
using System.Diagnostics;

namespace StockApp.Server.Services
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _context;

        public StockService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AreAnyStocksInDb()
        {
            return await _context.CachedStocks.AnyAsync();
        }

        //AU1hCQFcAbUpEiieLlv3UfW0U2c509e0

        public async Task<StockDTO> GetStockFromAPI(string symbol)
        {
            symbol = symbol.ToUpper();
            var apiKey = "AU1hCQFcAbUpEiieLlv3UfW0U2c509e0";
            HttpClient connection = new HttpClient();
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            var threeMonthsAgo = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
            try
            {
                var json = await connection.GetStringAsync($"https://api.polygon.io/v2/aggs/ticker/{symbol}/range/1/day/{threeMonthsAgo}/{today}?sort=asc&apiKey={apiKey}");
                Debug.WriteLine(json);
                var stock = JsonConvert.DeserializeObject<StockGet>(json);
                var tickersDTO = stock.Results.Select(e => new TickerDTO
                {
                    Volume = e.V,
                    VolumeAvg = e.Vw,
                    Open = e.O,
                    Close = e.C,
                    High = e.H,
                    Low = e.L,
                    Date = e.T,
                    TransactionCount = e.N
                });
                var tickerDeets = await connection.GetStringAsync($"https://api.polygon.io/v3/reference/tickers/{symbol}?apiKey={apiKey}");
                var jsonObject = JObject.Parse(tickerDeets);
                var name = jsonObject.SelectToken("results.name").Value<string>();
                var country = jsonObject.SelectToken("results.locale").Value<string>();
                var tags = jsonObject.SelectToken("results.sic_description").Value<string>();
                var assetType = jsonObject.SelectToken("results.market").Value<string>();
                var market = jsonObject.SelectToken("results.primary_exchange").Value<string>();
                var logoURL = jsonObject.SelectToken("results.branding.icon_url").Value<string>() + $"?apikey={apiKey}";

                var toReturn = new StockDTO
                {
                    Symbol = stock.Ticker,
                    Name = name,
                    Country = country,
                    LogoURL = logoURL,
                    Date = DateTime.Now,
                    Tags = tags,
                    Close = tickersDTO.First().Close,
                    Volume = tickersDTO.First().Volume,
                    Open = tickersDTO.First().Open,
                    High = tickersDTO.First().High,
                    Low = tickersDTO.First().Low,
                    Tickers = tickersDTO
                };
                //caching
                var row = _context.CachedStocks.Where(e => e.Symbol.Equals(toReturn.Symbol));

                if (row.Count() > 0)
                    _context.Remove(row);

                var tickers = tickersDTO.Select(e => new Ticker
                {
                    Volume = e.Volume,
                    VolumeAvg = e.VolumeAvg,
                    Open = e.Open,
                    Close = e.Close,
                    High = e.High,
                    Low = e.Low,
                    TransactionCount = e.TransactionCount,
                    Date = e.Date,
                    StockSymbol = symbol
                }).ToList();
                await _context.CachedStocks.AddAsync(new CachedStock
                {
                    Symbol = toReturn.Symbol,
                    Name = toReturn.Name,
                    Country = toReturn.Country,
                    LogoURL = logoURL,
                    Tags = toReturn.Tags,
                    Close = toReturn.Close,
                    Volume = toReturn.Volume,
                    Open = toReturn.Open,
                    High = toReturn.High,
                    Low = toReturn.Low,
                    AssetType = assetType,
                    Exchange = market,
                    UpdateTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Tickers = tickers
                });
                Debug.WriteLine(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        public async Task<StockDTO> GetStockFromDatabase(string symbol)
        {
            var stock = await _context.CachedStocks.FirstOrDefaultAsync(e => e.Symbol.Equals(symbol));
            var tickers = await _context.Tickers.Where(e => e.StockSymbol.Equals(symbol)).Select(e => new TickerDTO
            {
                Volume = e.Volume,
                VolumeAvg = e.VolumeAvg,
                Open = e.Open,
                Close = e.Close,
                High = e.High,
                Low = e.Low,
                Date = e.Date,
                TransactionCount = e.TransactionCount
            }).ToListAsync();
            if (tickers.Count() == 0)
                return null;

            tickers.OrderByDescending(e => e.Date);

            return new StockDTO
            {
                Symbol = stock.Symbol,
                Name = stock.Name,
                Country = stock.Country,
                LogoURL = stock.LogoURL,
                Tags = stock.Tags,
                Close = tickers.First().Close,
                Open = tickers.First().Open,
                High = tickers.First().High,
                Low = tickers.First().Low,
                Tickers = tickers
            };
        }

        public async Task<IEnumerable<StockSearchDTO>> GetStocks()
        {
            return await _context.CachedStocks.Select(e => new StockSearchDTO
            {
                Symbol = e.Symbol,
                Name = e.Name,
                AssetType = e.AssetType,
                Exchange = e.Exchange,
            }).ToListAsync();
        }

        public async Task<bool> IsStockInDb(string symbol)
        {
            var isPresent = await _context.CachedStocks.AnyAsync(e => e.Symbol.Equals(symbol));

            return isPresent;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
