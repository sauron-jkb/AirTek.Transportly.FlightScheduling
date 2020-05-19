using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AirTek.Transportly.FlightScheduling.Infrastructure.Model
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IDataContextFactory>(c => new DataContextFactory(configuration));

			return services;
		}
	}
}
