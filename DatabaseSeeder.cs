using System;
using System.Collections.Generic;
using SeedDatabase.Model;

namespace SeedDatabase
{
    public class DatabaseSeeder
    {
        private Random _random;
        public void Run()
        {
            _random = new Random();
            
            var locations = GenerateLocations();
            // PopulateLocationsTable(locations);
            var events = GenerateRandomEvents(locations);
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
        
        private List<Event> GenerateRandomEvents(List<Location> locations)
        {
            var events = new List<Event>();
            var totalNumDays = 180; // generate events for 6 months

            for (var day = 0; day < totalNumDays; day++)
            {
                var numEvents = _random.Next(0, 6);

                for (var j = 0; j < numEvents; j++)
                {
                    var date = DateTime.Today.AddDays(day);
                    var newEvent = GenerateRandomEvent(events.Count, locations, date);
                    events.Add(newEvent);
                }
            }

            return events;
        }

        private Event GenerateRandomEvent(int eventId, List<Location> locations, DateTime date)
        {
            var location = GetRandomLocation(locations);
            
            return new Event
            {
                EventId = eventId,
                TimeStamp = GetRandomTimeStamp(date),
                Flagged = GetRandomFlag(),
                LocationId = location.LocationId,
                Location = location
            };
        }

        private Location GetRandomLocation(List<Location> locations)
        {
            var index = _random.Next(locations.Count);
            return locations[index];
        }

        private bool GetRandomFlag()
        {
            return _random.NextDouble() >= 0.5;
        }

        private DateTime GetRandomTimeStamp(DateTime date)
        {
            var hour = _random.Next(0, 24);
            var minutes = _random.Next(0, 60);
            var seconds = _random.Next(0, 60);
            return new DateTime(date.Year, date.Month, date.Day, hour, minutes, seconds);
        }
    }
}