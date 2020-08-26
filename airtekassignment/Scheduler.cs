using System.Collections.Generic;
using System.Linq;


namespace airtekassignment
{
    public class Scheduler
    {
        private List<Flight> _flights;
        private List<Flight> flights
        {
            get
            {
                if (_flights == null)
                {
                    IDataProvider<Flight> flightsProvider = new FlightsProvider();
                    _flights = flightsProvider.GetData();
                }
                return _flights;  
            }
        }

        private List<Order> _orders;
        private List<Order> orders
        {
            get
            {
                if (_orders == null)
                {
                    IDataProvider<Order> ordersProvider = new OrdersProvider();
                    _orders = ordersProvider.GetData();
                }
                return _orders;
            }
        }


        public List<Flight> getScheduledFlights()
        {
            return flights;
        }

        public List<Destination> getScheduledData()
        {
            var destinations = new List<Destination>();
            var unscheduledOrders = new List<Order>();
            ProcessOrders(flights, orders, destinations, unscheduledOrders);
            return destinations;
        }

        public List<Order> getUnscheduledOrders()
        {
            var destinations = new List<Destination>();
            var unscheduledOrders = new List<Order>();       
            ProcessOrders(flights, orders, destinations, unscheduledOrders);
            return unscheduledOrders;
        }

        private void ProcessOrders(List<Flight> flights, List<Order> ordersList, List<Destination> destinations, List<Order> unscheduledOrders)
        {
            GroupFlights(flights, destinations);
            // Process orders and assign each order to an appropriate flight
            foreach (var order in ordersList)
            {
                var orderAdded = false;

                foreach (var destinationClass in destinations)
                {
                    if (order.destination == destinationClass.destinationAirport)
                    {
                        foreach (var flight in destinationClass.flights)
                        {
                            if (flight.orders.Count == 20)
                            {
                                continue;
                            }

                            flight.orders.Add(order);
                            orderAdded = true;
                            break;
                        }
                    }
                }

                if (!orderAdded)
                {
                    unscheduledOrders.Add(order);
                }
            }
        }

        private void GroupFlights(List<Flight> flightsList, List<Destination> destinationClasses)
        {
            // Group all flights into flights per destination
            for (int i = 0; i < flightsList.Count; i++)
            {
                var destinationClassForFlight = destinationClasses.FirstOrDefault(destClass => destClass.destinationAirport == flightsList[i].destination);
                if (destinationClassForFlight == null)
                {
                    var destinationClass = new Destination
                    {
                        destinationAirport = flightsList[i].destination,
                        originAirport = flightsList[i].origin
                    };
                    destinationClass.flights.Add(new Flight { flightNumber = i + 1, day = flightsList[i].day });
                    destinationClasses.Add(destinationClass);
                }
                else
                {
                    destinationClassForFlight.flights.Add(new Flight { flightNumber = i + 1, day = flightsList[i].day });
                }
            }
        }
    }
}
