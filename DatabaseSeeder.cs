using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            PopulateLocationsTable(locations);
            
            var events = GenerateRandomEvents(locations);
            PopulateEventsTable(events);
            
            ReadEventsTable();
        }
        
        private List<Location> GenerateLocations()
        {
            return new List<Location>
            {
                new Location
                {
                    Name = "Garage"
                },
                new Location
                {
                    Name = "Thermostat"
                },
                new Location
                {
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
                    var newEvent = GenerateRandomEvent(locations, date);
                    events.Add(newEvent);
                }
            }

            return events;
        }

        private Event GenerateRandomEvent(List<Location> locations, DateTime date)
        {
            var location = GetRandomLocation(locations);
            
            return new Event
            {
                TimeStamp = GetRandomTimeStamp(date),
                Flagged = GetRandomFlag(),
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

        private void ReadEventsTable()
        {
            try
            {
                using var db = new EventsContext();
                var events = db.Events
                    .Include("Location")
                    .OrderBy(e => e.TimeStamp)
                    .ToList();
                events.ForEach(e =>
                {
                    Console.WriteLine($"{e.TimeStamp} - {e.Location.Name}");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading data: " + ex.Message);
            }
            
        }

        private void PopulateLocationsTable(List<Location> locations)
        {
            using var db = new EventsContext();
            db.AddLocations(locations);
        }

        private void PopulateEventsTable(List<Event> events)
        {
            using var db = new EventsContext();
            db.AddEvents(events);
        }
    }
}