
namespace AirTek.Transportly.FlightScheduling.Presentation.Console
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading.Tasks;

	using MediatR;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using AirTek.Transportly.FlightScheduling.Core.Application.Flights.Queries.GetFlights;
	using AirTek.Transportly.FlightScheduling.Core.Application.Orders.Queries.GetOrders;
	using AirTek.Transportly.FlightScheduling.Infrastructure.Model;
	
	class Program
	{
		static IConfigurationRoot Configuration { get; set; }

		static async Task Main(string[] args)
		{
			Console.WriteLine("Hello AirTek!");
			Console.WriteLine("");

			string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			string launch = Environment.GetEnvironmentVariable("LAUNCH_PROFILE");

			if (string.IsNullOrWhiteSpace(env))
			{
				env = "Development";
			}

			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables(); ;

			if (args != null)
			{
				configuration.AddCommandLine(args);
			}

			Configuration = configuration.Build();

			ServiceCollection serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);

			ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

			IMediator Mediator = serviceProvider.GetService<IMediator>();

			List<Flight> flights = await Mediator.Send(new GetFlightsQuery());
			if (flights != null)
			{
				Console.WriteLine("USER STORY #1");
				Console.WriteLine("------------------------------------------------------------------------");
				foreach (Flight flight in flights)
				{
					Console.WriteLine("Flight: {0}, departure: {1}, arrival: {2}, day: {3}", flight.FlightNumber, flight.Origin, flight.Destination, (flight.FlightDay.HasValue) ? flight.FlightDay.Value.ToString() : string.Empty);
				}
			}

			Console.WriteLine("");

			List<OrderItinerary> orders = await Mediator.Send(new GetOrdersQuery());
			if (orders != null)
			{
				Console.WriteLine("USER STORY #2");
				Console.WriteLine("------------------------------------------------------------------------");
				foreach(OrderItinerary order in orders)
				{
					Console.WriteLine("order: {0}, flightNumber: {1}, departure: {2}, arrival: {3}, day: {4}", order.OrderLabel, order.FlightNumber, order.Origin, order.Destination, order.FlightDay);
				}
			}
		}



		private static void ConfigureServices(IServiceCollection services)
		{
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
			services.AddSingleton<IConfiguration>(Configuration);

			AppSettings appSettings = new AppSettings();
			Configuration.GetSection("AppSettings").Bind(appSettings);

			services.AddMediatR(typeof(GetFlightsQuery));
			services.AddPersistence(Configuration);
		}
	}
}
