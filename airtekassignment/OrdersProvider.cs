using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace airtekassignment
{
    internal class OrdersProvider : IDataProvider<Order>
    {
        public List<Order> GetData()
        {
            var ordersString = File.ReadAllText("files/orders.json");

            var ordersDictionary = JsonSerializer.Deserialize<Dictionary<string, Order>>(ordersString);

            var ordersList = new List<Order>();

            foreach (var order in ordersDictionary)
            {
                order.Value.orderNumber = order.Key;
                ordersList.Add(order.Value);
            }
            return ordersList;
        }
    }
}
