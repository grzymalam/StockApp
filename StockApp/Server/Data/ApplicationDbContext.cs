using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StockApp.Server.Data.Models;
using StockApp.Server.Models;

namespace StockApp.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CachedStock>(e =>
            {
                e.ToTable("CachedStocks");

                e.HasKey(cs => cs.Symbol);
            });

            builder.Entity<UserStock>(e =>
            {
                e.ToTable("UserStocks");

                e.HasKey(us => new { us.ApplicationUserId, us.Symbol });
                
                e.HasOne(p => p.ApplicationUser).WithMany(user => user.UserStocks).HasForeignKey(p => p.ApplicationUserId);
                e.HasOne(p => p.CachedStock).WithMany(cs => cs.UserStocks).HasForeignKey(p => p.Symbol);
            });
            builder.Entity<Ticker>(e =>
            {
                e.ToTable("Tickers");

                e.HasKey(t => t.Id);
                e.HasOne(t => t.Stock).WithMany(s => s.Tickers).HasForeignKey(t => t.StockSymbol);
            });
        }
        public DbSet<CachedStock> CachedStocks { get; set; }
        public DbSet<UserStock> UserStocks { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Ticker> Tickers { get; set; }
    }
}