using Microsoft.AspNetCore.Mvc;
using StockApp.Server.Services;

namespace StockApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _service;

        public ArticlesController(IArticleService service)
        {
            _service = service;
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetArticles(string symbol)
        {
            symbol = symbol.ToUpper();
            var articles = await _service.GetArticles(symbol);
            if (articles is null)
                return NotFound("No articles for given stock.");
            return Ok(articles);
        }
    }
}
