using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class AirlinesManager : IAirlinesManager
    {
        //Fields
        private Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
        private Dictionary<string, Flight> flights = new Dictionary<string, Flight>();

        //Methods
        public void AddAirline(Airline airline)
        {
            airlines.Add(airline.Id, airline);
        }

        public void AddFlight(Airline airline, Flight flight)
        {
            if (!airlines.ContainsKey(airline.Id))
            {
                throw new ArgumentException();
            }
            airline.Flights.Add(flight);
            flights.Add(flight.Id, flight);
        }

        public bool Contains(Airline airline) => airlines.ContainsKey(airline.Id);

        public bool Contains(Flight flight) => flights.ContainsKey(flight.Id);

        public void DeleteAirline(Airline airline)
        {
            if (!airlines.ContainsKey(airline.Id))
            {
                throw new ArgumentException();
            }

            foreach (var flight in airline.Flights)
            {
                flights.Remove(flight.Id);
            }

            airline.Flights = null;
            airlines.Remove(airline.Id);
        }

        public IEnumerable<Airline> GetAirlinesOrderedByRatingThenByCountOfFlightsThenByName()
        {
            if (airlines.Count == 0)
            {
                return airlines.Values;
            }
            return airlines.Values.OrderByDescending(a => a.Rating)
                .ThenByDescending(a => a.Flights.Count)
                .ThenBy(a => a.Name);
        }

        public IEnumerable<Airline> GetAirlinesWithFlightsFromOriginToDestination(string origin, string destination) =>
            airlines.Select(a => a.Value)
            .Where(a => a.Flights.Any(f => f.Destination == destination && f.Origin == origin && !f.IsCompleted));

        public IEnumerable<Flight> GetAllFlights() => flights.Values;

        public IEnumerable<Flight> GetCompletedFlights() =>
            flights.Values.Where(f => f.IsCompleted == true);

        public IEnumerable<Flight> GetFlightsOrderedByCompletionThenByNumber()
        {
            if (flights.Count == 0)
            {
                return flights.Values;
            }

            return flights.Values.OrderBy(f => f.IsCompleted).ThenBy(f => f.Number);
        }

        public Flight PerformFlight(Airline airline, Flight flight)
        {
            if (!airlines.ContainsKey(airline.Id) || !flights.ContainsKey(flight.Id))
            {
                throw new ArgumentException();
            }
            flights[flight.Id].IsCompleted = true;

            return flights[flight.Id];
        }
    }
}