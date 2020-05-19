
using System.Collections.Generic;

namespace AirTek.Transportly.FlightScheduling.Infrastructure.Model
{
	public class Flight : Entity
	{
		public string FlightNumber { get; set; }
		public int? FlightDay { get; set; }
		public string Origin { get; set; }
		public string Destination { get; set; }
		public List<Order> Orders { get; set; }
	}
}
