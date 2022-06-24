namespace StockApp.Server.Models
{
    public class UserStock
    {
        public string Symbol { get; set; }
        public virtual CachedStock CachedStock { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
