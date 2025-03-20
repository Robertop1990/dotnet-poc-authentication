using Microsoft.EntityFrameworkCore;
using WU.Poc.Authentication.Domain;

namespace WU.Poc.Authentication.Infrastructure
{
    public class WUDbContext : DbContext
    {
        public WUDbContext(DbContextOptions<WUDbContext> options) : base(options) {}

        public DbSet<ApiKey> ApiKeys { get; set; }
    }
}
