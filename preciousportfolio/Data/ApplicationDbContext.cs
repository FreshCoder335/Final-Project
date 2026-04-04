using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using preciousportfolio.Models;

namespace preciousportfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Stores active holdings (inventory)
        public DbSet<Holding> Holdings { get; set; }

        // Stores completed sale transactions (NEW)
        public DbSet<SaleTransaction> SaleTransactions { get; set; }
    }
}