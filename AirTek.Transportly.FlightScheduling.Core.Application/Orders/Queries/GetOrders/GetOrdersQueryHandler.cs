
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using AirTek.Transportly.FlightScheduling.Infrastructure.Model;

namespace AirTek.Transportly.FlightScheduling.Core.Application.Orders.Queries.GetOrders
{
	public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderItinerary>>
	{
		private readonly DataContext _dataContext;

		public GetOrdersQueryHandler(IDataContextFactory dataContextFactory)
		{
			_dataContext = dataContextFactory.GetDataContext();
		}

		public async Task<List<OrderItinerary>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
		{
			List<OrderItinerary> orderItineraries = new List<OrderItinerary>();

			try
			{
				// order: order-001, flightNumber: 1, departure: <departure_city>, arrival: <arrival_city>, day: x
				orderItineraries =
					(
						from fs in _dataContext.FlightSchedules
						from o in fs.Orders
						select new OrderItinerary
						{
							Destination = fs.Destination,
							FlightDay = fs.FlightDay,
							FlightNumber = fs.FlightNumber,
							OrderLabel = o.OrderLabel,
							Origin = fs.Origin
						}
					)
					.OrderBy(o => o.OrderLabel)
					.ToList();
			}
			catch(Exception ex)
			{
				Console.WriteLine("Uh-oh! Error: {0}", ex.Message);
			}

			return orderItineraries;
		}
	}
}
