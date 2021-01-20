using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SeedDatabase.Model;

namespace SeedDatabase
{
    public class EventsContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }

        public void AddLocations(List<Location> locations)
        {
            this.Locations.AddRange(locations);
            this.SaveChanges();
        }
        
        public void AddEvents(List<Event> events) {
            this.Events.AddRange(events);
            this.SaveChanges();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            optionsBuilder.UseSqlServer(@config["EventsContext:ConnectionString"]);
        }
    }
}