using System.Collections.Generic;


namespace airtekassignment
{
    public class Destination
    {
        public List<Flight> flights { get; } = new List<Flight>();
        public string destinationAirport { get; set; }
        public string originAirport { get; set; }
    }
}
