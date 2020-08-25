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

                dynamic flightsObject = JsonConvert.DeserializeObject<FlightSchedule>(flightsString); 
 
                Console.WriteLine(flightsObject);

                Console.WriteLine("loading flights...complete");

                var displayFlightsInput = string.Empty;                    
                do
                {
                    Console.WriteLine("do you wish to display the flights? (Y/N)");
                    displayFlightsInput = (Console.ReadLine()).ToUpperInvariant();
                    
                } while (displayFlightsInput != "Y" && displayFlightsInput != "N");

                if (displayFlightsInput == "N")
                {
                    Console.WriteLine("Thanks for using our system");
                }
                else
                {
                    Console.WriteLine("generating list");   
                }
                
            }

        }
    }

    public class Flight
    {
        public string origin { get; set; }
        public string destination { get; set; }
        public string date { get; set; }
    }

    public class FlightSchedule
    {
        public List<Flight> Flights { get; set; }

    }
    
}
