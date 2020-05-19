
namespace AirTek.Transportly.FlightScheduling.Infrastructure.Model
{
	public class OrderItinerary
	{
		public string OrderLabel { get; set; }
		public string FlightNumber { get; set; }
		public string Origin { get; set; }
		public string Destination { get; set; }
		public int? FlightDay { get; set; }
	}
}
