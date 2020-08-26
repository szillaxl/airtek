using System.Collections.Generic;


namespace airtekassignment
{
    public class Flight
    {
        public List<Order> orders { get; } = new List<Order>();
        public int flightNumber { get; set; }
        public string day { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
    }
}
