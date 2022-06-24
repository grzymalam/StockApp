using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StockApp.Server.Services;

namespace StockApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        
        public UsersController(IUserService service)
        {
            _service = service;
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GetClientStocks(string username)
        { 
            var clientStocks = await _service.GetClientStocks(username);
            if (clientStocks.Count() == 0)
                return NotFound("No stocks for given user!");
            return Ok(await _service.GetClientStocks(username));
        }
        
        [HttpPost("{username}/{symbol}")]
        public async Task<IActionResult> AddStockToWatchlist(string username, string symbol)
        {
            var doesStockExist = await _service.DoesStockExist(symbol);
            if (!doesStockExist)
                return NotFound("Given stock doesn't exist.");
            var isAlreadyWatched = await _service.DoesUserWatchStock(username, symbol);
            if (isAlreadyWatched)
                return StatusCode(409);
            await _service.AddStockToWatchlistForUser(username, symbol);
            await _service.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{username}/{symbol}")]
        public async Task<IActionResult> RemoveStockFromWatchlist(string username, string symbol)
        {
            var doesUserWatchStock = await _service.DoesUserWatchStock(username, symbol);
            if (!doesUserWatchStock)
                return NotFound("You aren't watching the given stock.");
            await _service.RemoveStockFromWatchlist(username, symbol);
            await _service.SaveChangesAsync();
            return Ok();
        }
    }
}
