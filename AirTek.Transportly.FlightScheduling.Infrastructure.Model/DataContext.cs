
using System.Collections.Generic;
using System.Data.Entity;

namespace AirTek.Transportly.FlightScheduling.Infrastructure.Model
{
	public class DataContext : DbContext, IDataContext
	{
		bool _disposed;

        public List<Flight> FlightSchedules;

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dump managed
                }

                // Dump unmanaged
                _disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
