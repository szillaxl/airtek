using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace airtekassignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("loading flights");

            using (StreamReader flightsJson = new StreamReader("files/flights.json"))
            {
                var flightsString = flightsJson.ReadToEnd();

                var flightsList = JsonConvert.DeserializeObject<List<Flight>>(flightsString);

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

                StreamReader ordersJson = new StreamReader("files/orders.json");
                var ordersString = ordersJson.ReadToEnd();
                var ordersList = JsonConvert.DeserializeObject<Dictionary<string,Order>>(ordersString);
                foreach (var order in ordersList)
                {
                    var x = 0;
                    foreach (var flight in flightsList)
                    {
                        x++;
                        if (order.Value.destination == flight.destination)
                        {
                           Console.WriteLine($"{order.Value.destination} - {flight.destination} match");
                           break;
                        }
                        if (x == flightsList.Count)
                        {
                            Console.WriteLine($"order: {order.Key}, flightNumber: not scheduled");
                        }
                        
                    }

                 }

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
    }
}
