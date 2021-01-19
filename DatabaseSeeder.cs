using System.Collections.Generic;
using SeedDatabase.Model;

namespace SeedDatabase
{
    public class DatabaseSeeder
    {
        public void Run()
        {
            List<Location> locations = GenerateLocations();
            // PopulateLocationsTable(locations);
            // List<Event> events = GenerateRandomEvents();
            // PopulateEventsTable(events);
            // ReadEventsTable();
        }
        
        private List<Location> GenerateLocations()
        {
            return new List<Location>
            {
                new Location
                {
                    LocationId = 0, 
                    Name = "Garage"
                },
                new Location
                {
                    LocationId = 1, 
                    Name = "Thermostat"
                },
                new Location
                {
                    LocationId = 2, 
                    Name = "Kitchen Light"
                }
            };
        }
    }
}