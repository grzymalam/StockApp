namespace StockApp.Shared
{
    public class StockDTO
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string LogoURL { get; set; }
        public string Tags { get; set; }
        public DateTime Date { get; set; }
        public float Close { get; set; }
        public float Volume { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public IEnumerable<TickerDTO> Tickers { get; set; }
    }
}
