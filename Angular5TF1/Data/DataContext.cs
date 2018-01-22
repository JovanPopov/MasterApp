using Angular5TF1.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular5TF1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SearchTerm> SearchTerms { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Wikipedia> Wikipedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchTerm>()
            .HasOne(p => p.Wikipedia)
            .WithOne(b => b.SearchTerm)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SearchTerm>()
           .HasMany(p => p.Events)
           .WithOne(b => b.SearchTerm)
           .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
