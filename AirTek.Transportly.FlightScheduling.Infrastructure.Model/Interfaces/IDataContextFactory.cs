using System;

namespace AirTek.Transportly.FlightScheduling.Infrastructure.Model
{
	public interface IDataContextFactory : IDisposable
	{
		DataContext GetDataContext();
	}
}
