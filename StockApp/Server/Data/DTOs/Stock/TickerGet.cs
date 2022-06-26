using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace StockApp.Server.Data.DTOs.Stock
{
    public class TickerGet
    {
        [JsonProperty("v")]
        public long V { get; set; }
        [JsonProperty("vw")]

        public float Vw { get; set; }
        [JsonProperty("o")]

        public float O { get; set; }
        [JsonProperty("c")]

        public float C { get; set; }
        [JsonProperty("h")]

        public float H { get; set; }
        [JsonProperty("l")]
        
        public float L { get; set; }
        [JsonProperty("t")]
        
        public long T { get; set; }
        [JsonProperty("n")]
        
        public int N { get; set; }
    }
}
