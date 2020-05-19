
using System;
using System.Collections.Generic;

namespace AirTek.Transportly.FlightScheduling.Infrastructure.Model
{
	public class FlightLeg : Entity
	{
		public FlightLeg()
		{
			this.Orders = new List<Order>();
		}

		public string OriginAirport { get; set; }
		public DateTime DepartureTime { get; set; }
		public string DestinationAirport { get; set; }
		public DateTime ArrivalTime { get; set; }
		public List<Order> Orders { get; set; }
	}
}
