using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockApp.Server.Data;
using StockApp.Server.Data.DTOs.Article;
using StockApp.Shared;

namespace StockApp.Server.Services
{
    public class ArticleService:IArticleService
    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public async Task<IEnumerable<ArticleDTO>> GetArticles(string symbol)
        {
            var apiKey = "AU1hCQFcAbUpEiieLlv3UfW0U2c509e0";
            HttpClient connection = new HttpClient();
            var response = await connection.GetAsync($"https://api.polygon.io/v2/reference/news?ticker={symbol}&apiKey={apiKey}");
            if (!response.IsSuccessStatusCode)
                return null;
            var articleDeets = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResultGet>(articleDeets).Results;
            List<ArticleDTO> articles = new List<ArticleDTO>();
            foreach(var result in results)
            {
                var article = new ArticleDTO
                {
                    Title = result.Title,
                    Publisher = result.Publisher.Name,
                    DatePublished = result.DatePublished,
                    URL = result.URL
                };
                articles.Add(article);
            }

            return articles;
        }
    }
}
