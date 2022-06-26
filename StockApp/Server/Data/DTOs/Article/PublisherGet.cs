using Newtonsoft.Json;

namespace StockApp.Server.Data.DTOs.Article
{
    public class PublisherGet
    {
        [JsonProperty("name")]
        public string Name { get; set; }   
    }
}
