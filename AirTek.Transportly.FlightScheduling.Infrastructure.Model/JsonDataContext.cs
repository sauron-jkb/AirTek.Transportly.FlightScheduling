
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

namespace AirTek.Transportly.FlightScheduling.Infrastructure.Model
{
	public class JsonDataContext : DataContext
	{
        public JsonDataContext() : base()
        {
            FlightSchedules = new List<Flight>();
            
            // By convention the data file name will be orders.json
            string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "orders.json");
            
            string json = File.ReadAllText(path);
            SortedDictionary<string, OrderDestination> orders = JsonConvert.DeserializeObject<SortedDictionary<string, OrderDestination>>(json);

            // NOTE: The json data file does not contain the advertised number of data items
            //foreach (KeyValuePair<string, OrderDestination> order in orders)
            //{
            //    Console.WriteLine("Key: {0}, Value: {1}", order.Key, ((OrderDestination)order.Value).Destination);
            //}

            // NOTE: The json data file contains a destination not in the requirements
            //List<string> destinations =
            //    (
            //        from o in orders
            //        select o.Value.Destination
            //    )
            //    .Distinct()
            //    .ToList();

            List<string> destinations = new List<string>();
            destinations.Add("YYZ");
            destinations.Add("YYC");
            destinations.Add("YVR");

            int flightNumber = 1;
            foreach (string destination in destinations)
            {
                List<List<BaseOrder>> destinationOrderLists =
                    (
                        from o in orders
                        where o.Value.Destination == destination
                        select new BaseOrder
                        {
                            OrderLabel = o.Key,
                            OrderDestination = new OrderDestination
                            {
                                Destination = o.Value.Destination
                            }
                        }
                    )
                    .ToList()
                    .ChunkBy<BaseOrder>(20);

                int dayCounter = 1;
                destinationOrderLists.ForEach(destinationOrderList =>
                {
                    Flight flight = new Flight
                    {
                        FlightNumber = (dayCounter < 3) ? flightNumber.ToString() : "not scheduled",
                        FlightDay = (dayCounter < 3) ? dayCounter : (int?)null,
                        Destination = destination,
                        Origin = "YUL",
                        Orders = 
                        (
                            from dol in destinationOrderList
                            select new Order { BoxQuantity = 1, OrderLabel = dol.OrderLabel }
                        )
                        .ToList()
                    };

                    FlightSchedules.Add(flight);
                    dayCounter++;
                    flightNumber++;
                });
            }
        }
    }
}
