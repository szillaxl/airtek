using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace airtekassignment
{
    internal class FlightsProvider : IDataProvider<Flight>
    {
        public List<Flight> GetData()
        {
            var flightsString = File.ReadAllText("files/flights.json");
            return JsonSerializer.Deserialize<List<Flight>>(flightsString);
        }
    }
}
