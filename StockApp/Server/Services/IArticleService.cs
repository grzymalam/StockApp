using StockApp.Shared;

namespace StockApp.Server.Services
{
    public interface IArticleService
    {
        public Task<IEnumerable<ArticleDTO>> GetArticles(string symbol);
    }
}
