using System;
using System.Collections.Generic;


namespace airtekassignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("loading flights");
            var program =  new Scheduler();
            var scheduledflights =  program.getScheduledFlights();
            Console.WriteLine("loading flights...complete");
            GetInput("Do you want to display the flight list?");
            Console.WriteLine("generating list");
            DisplayFlightsList(scheduledflights);
            GetInput("Do you want to display the orders list?");
            var scheduledData = program.getScheduledData();
            DisplayOrdersSchedule(scheduledData);
            var unscheduledOrders = program.getUnscheduledOrders();
            DisplayUnscheduledOrders(unscheduledOrders);
        }

        private static void DisplayFlightsList(List<Flight> flightsList)
        {
            var flightCounter = 1;
            foreach (var flight in flightsList)
            {
                Console.WriteLine($"Flight: {flightCounter}, departure: {flight.origin}, arrival: {flight.destination}, day: {flight.day}");
                flightCounter++;
            }
        }

        private static void DisplayOrdersSchedule(List<Destination> destinationClasses)
        {
            // display the order schedules
            foreach (var destination in destinationClasses)
            {
                foreach (var flight in destination.flights)
                {
                    foreach (var order in flight.orders)
                    {
                        Console.WriteLine($"order: {order.orderNumber}, flightNumber: {flight.flightNumber}, departure: {destination.originAirport}, arrival: {destination.destinationAirport}, day: {flight.day}");
                    }
                }
            }
        }

        private static void DisplayUnscheduledOrders(List<Order> unscheduledOrders)
        {
            // display the unscheduled orders
            foreach (var order in unscheduledOrders)
            {
                Console.WriteLine($"order: {order.orderNumber}, flightNumber: not scheduled");
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
