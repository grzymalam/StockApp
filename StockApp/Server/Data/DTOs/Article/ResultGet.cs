using Newtonsoft.Json;

namespace StockApp.Server.Data.DTOs.Article
{
    public class ResultGet
    {
        [JsonProperty("results")]
        public List<ArticleGet> Results { get; set; }
    }
}
