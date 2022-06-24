using Newtonsoft.Json;

namespace StockApp.Server.Data.DTOs.Stock
{
    public class StockGet
    {
        [JsonProperty("ticker")]

        public string Ticker { get; set; }
        [JsonProperty("queryCount")]

        public int QueryCount { get; set; }
        [JsonProperty("resultsCount")]

        public int ResultsCount { get; set; }
        [JsonProperty("adjusted")]

        public bool Adjusted { get; set; }
        [JsonProperty("results")]

        public List<TickerGet> Results { get; set; }
        [JsonProperty("status")]

        public string Status { get; set; }
        [JsonProperty("request_id")]

        public string Request_id { get; set; }
        [JsonProperty("count")]

        public int Count { get; set; }
    }
}
