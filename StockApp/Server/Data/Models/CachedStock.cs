using StockApp.Server.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockApp.Server.Models
{
    public class CachedStock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string LogoURL { get; set; }
        public string Tags { get; set; }
        public float Close { get; set; }
        public float Volume { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public long UpdateTime { get; set; }
        public string Exchange { get; set; }
        public string AssetType { get; set; }
        public IEnumerable<UserStock> UserStocks { get; set; }
        public IEnumerable<Ticker> Tickers { get; set; }

    }
}
