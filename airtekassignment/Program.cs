using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace airtekassignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("loading flights");

            var flightsString = File.ReadAllText("files/flights.json");

            var flightsList = JsonSerializer.Deserialize<List<Flight>>(flightsString);

            Console.WriteLine("loading flights...complete");

            GetInput("Do you want to display the flight list?");

            Console.WriteLine("generating list");

            var flightCounter = 1;
            foreach (var flight in flightsList)
            {
                Console.WriteLine($"Flight: {flightCounter}, departure: {flight.origin}, arrival: {flight.destination}, day: {flight.date}");
                flightCounter++;
            }

            GetInput("Do you want to display the orders list?");

            var ordersString = File.ReadAllText("files/orders.json");
            
            var ordersList = JsonSerializer.Deserialize<Dictionary<string,Order>>(ordersString);

            var destinationClasses = new List<DestinationClass>();
            var unscheduledOrders = new Dictionary<string, Order>();


            // Group all flights into flights per destination
            for (int i = 0; i < flightsList.Count; i++)
            {
                var destinationClassForFlight = destinationClasses.FirstOrDefault(destClass => destClass.destinationAirport == flightsList[i].destination);
                if (destinationClassForFlight == null)
                {
                    var destinationClass = new DestinationClass
                    {
                        destinationAirport = flightsList[i].destination,
                        originAirport = flightsList[i].origin
                    };
                    destinationClass.flights.Add(new FlightClass{ flightNumber = i + 1, day = flightsList[i].date });
                    destinationClasses.Add(destinationClass);
                }
                else
                {
                    destinationClassForFlight.flights.Add(new FlightClass { flightNumber = i + 1, day = flightsList[i].date });
                }
            }


            // Process orders and assign each order to an appropriate flight
            foreach (var order in ordersList)
            {
                var orderAdded = false;
                
                foreach (var destinationClass in destinationClasses)
                {
                    if (order.Value.destination == destinationClass.destinationAirport)
                    {
                        foreach (var flight in destinationClass.flights)
                        {
                            if (flight.orders.Count == 20)
                            {
                                continue;
                            }

                            flight.orders[order.Key] = order.Value;
                            orderAdded = true;
                            break;
                        }
                    }
                }

                if (!orderAdded)
                {
                    unscheduledOrders[order.Key] = order.Value;
                }
            }

            // display the order schedules
            foreach (var destination in destinationClasses)
            {
                foreach (var flight in destination.flights)
                {
                    foreach (var order in flight.orders)
                    {
                        Console.WriteLine($"order: {order.Key}, flightNumber: {flight.flightNumber}, departure: {destination.originAirport}, arrival: {destination.destinationAirport}, day: {flight.day}");
                    }
                }
            }

            // display the unscheduled orders
            foreach (var order in unscheduledOrders)
            {
                Console.WriteLine($"order: {order.Key}, flightNumber: not scheduled");
            }
            
        }

        private static void GetInput(string promt)
        {
            var input = string.Empty;
            do
            {
                Console.WriteLine(promt + "(Y/N)");
                input = (Console.ReadLine()).ToUpperInvariant();

            } while (input != "Y" && input != "N");

            if (input == "N")
            {
                Console.WriteLine("Thanks for using our system");
                Environment.Exit(0);
            }
        }

        private class FlightClass
        {
            public Dictionary<string, Order> orders { get; } = new Dictionary<string, Order>();
            public int flightNumber { get; set; }
            public string day { get; set; }
        }

        private class DestinationClass
        {
            public List<FlightClass> flights { get; } = new List<FlightClass>();
            public string destinationAirport { get; set; }
            public string originAirport { get; set; }
        }

    }
}
