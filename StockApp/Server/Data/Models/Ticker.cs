using StockApp.Server.Models;

namespace StockApp.Server.Data.Models
{
    public class Ticker
    {
        public int Id { get; set; }
        public long Volume { get; set; }
        public float VolumeAvg { get; set; }
        public float Open { get; set; }
        public float Close { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public int TransactionCount { get; set; }
        public string StockSymbol { get; set; }
        public long Date { get; set; }
        public virtual CachedStock Stock { get; set; }
    }
}
