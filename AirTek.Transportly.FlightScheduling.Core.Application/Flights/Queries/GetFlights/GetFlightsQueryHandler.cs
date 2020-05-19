
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using AirTek.Transportly.FlightScheduling.Infrastructure.Model;

namespace AirTek.Transportly.FlightScheduling.Core.Application.Flights.Queries.GetFlights
{
	public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, List<Flight>>
	{
		private readonly DataContext _dataContext;
		
		public GetFlightsQueryHandler(IDataContextFactory dataContextFactory)
		{
			_dataContext = dataContextFactory.GetDataContext();
		}

		public async Task<List<Flight>> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
		{
			List<Flight> flights = new List<Flight>();

			try
			{
				flights =
					(
						from fs in _dataContext.FlightSchedules
						select fs
					)
					.OrderBy(fs => fs.FlightNumber).ThenBy(fs => fs.Destination).ThenBy(fs => fs.FlightDay)
					.ToList();
			}
			catch(Exception ex)
			{
				Console.WriteLine("Uh-oh! Error: {0}", ex.Message);
			}

			return flights;
		}
	}
}
