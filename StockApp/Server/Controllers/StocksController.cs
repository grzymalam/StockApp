using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Server.Services;
using StockApp.Shared;
using System.Diagnostics;

namespace StockApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _service;

        public StocksController(IStockService service)
        {
            _service = service;
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetStockInfo(string symbol)
        {
            var stockFromApi = await _service.GetStockFromAPI(symbol);
            if (stockFromApi == null)
            {
                var isStockInDb = await _service.IsStockInDb(symbol);
                if (!isStockInDb)
                    return NotFound("The stock couldn't be found. (api is down and it's not present in db)");
                var stockFromDb = await _service.GetStockFromDatabase(symbol);
                return Ok(stockFromDb);
            }
            await _service.SaveChangesAsync();
            return Ok(stockFromApi);
        }
        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var stocksPresentInDb = await _service.AreAnyStocksInDb();
            if (!stocksPresentInDb)
                return NotFound("No stocks in db.");
            return Ok(await _service.GetStocks());
        }
    }
}
