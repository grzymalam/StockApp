using Microsoft.AspNetCore.Identity;

namespace StockApp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<UserStock> UserStocks { get; set; }
    }
}