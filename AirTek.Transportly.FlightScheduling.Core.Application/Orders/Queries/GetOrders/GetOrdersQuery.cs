
using System.Collections.Generic;

using MediatR;

using AirTek.Transportly.FlightScheduling.Infrastructure.Model;

namespace AirTek.Transportly.FlightScheduling.Core.Application.Orders.Queries.GetOrders
{
	public class GetOrdersQuery : IRequest<List<OrderItinerary>>
	{
		
	}
}
