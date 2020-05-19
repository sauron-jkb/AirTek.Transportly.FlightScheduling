
using System.Collections.Generic;

using MediatR;

using AirTek.Transportly.FlightScheduling.Infrastructure.Model;

namespace AirTek.Transportly.FlightScheduling.Core.Application.Flights.Queries.GetFlights
{
	public class GetFlightsQuery : IRequest<List<Flight>>
	{
	}
}
