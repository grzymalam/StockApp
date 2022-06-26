using Newtonsoft.Json;

namespace StockApp.Server.Data.DTOs.Article
{
    public class ArticleGet
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("published_utc")]
        public DateTime DatePublished { get; set; }
        [JsonProperty("article_url")]
        public string URL { get; set; }
        [JsonProperty("publisher")]
        public PublisherGet Publisher { get; set; }
    }
}
