using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StockApp.Server.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace StockApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersController(IUserService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> GetClientStocks()
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var clientStocks = await _service.GetClientStocks(username);
            if (clientStocks.Count() == 0)
                return NotFound("No stocks for given user!");
            return Ok(await _service.GetClientStocks(username));
        }
        
        [HttpPost("{symbol}")]
        public async Task<IActionResult> AddStockToWatchlist(string symbol)
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
        [HttpDelete("{symbol}")]
        public async Task<IActionResult> RemoveStockFromWatchlist(string symbol)
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var doesUserWatchStock = await _service.DoesUserWatchStock(username, symbol);
            if (!doesUserWatchStock)
                return NotFound("You aren't watching the given stock.");
            await _service.RemoveStockFromWatchlist(username, symbol);
            await _service.SaveChangesAsync();
            return Ok();
        }
    }
}
