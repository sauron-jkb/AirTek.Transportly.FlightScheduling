
namespace AirTek.Transportly.FlightScheduling.Infrastructure.Model
{
	public class Order : Entity
	{
		public int OrderId { get; set; }
		public string OrderLabel { get; set; }
		public int BoxQuantity { get; set; }
	}
}
