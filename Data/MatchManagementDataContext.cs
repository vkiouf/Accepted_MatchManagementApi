using MatchManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchManagementApi.Data
{
    public class MatchManagementDataContext : DbContext
    {
        public MatchManagementDataContext(DbContextOptions<MatchManagementDataContext> options) : base(options) 
        { }


        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchOdds> MatchOddss { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>().ToTable("Match");
            modelBuilder.Entity<MatchOdds>().ToTable("MatchOdds");
        }
    }
}
